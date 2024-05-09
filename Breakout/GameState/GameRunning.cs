namespace Breakout.GameState;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using Breakout;
using DIKUArcade.Input;
using LevelLoading;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout.GameState;



public class GameRunning : IGameState {

    private static GameRunning instance = null;
    private GameEventBus eventBus;
    
    private string levelName, levelPath;

    private LevelData levelData;
    private LoadLevel levelLoader = new LoadLevel();
    private BlockContainer container = new BlockContainer();
    private Player player;

    public static GameRunning GetInstance() {
        if (instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
            default:
                break;
        }
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                GameEvent moveLeft = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "MOVE_LEFT"
                };
                eventBus.RegisterEvent(moveLeft);
                break;

            case KeyboardKey.Right:
                GameEvent moveRight = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "MOVE_RIGHT"
                };
                eventBus.RegisterEvent(moveRight);
                break;
            case KeyboardKey.Escape:
                BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        });
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                GameEvent stopMoveLeft = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "STOP_MOVE_LEFT"
                };
                eventBus.RegisterEvent(stopMoveLeft);
                break;

            case KeyboardKey.Right:
                GameEvent stopMoveRight = new GameEvent {
                    EventType = GameEventType.MovementEvent,
                    Message = "STOP_MOVE_RIGHT"
                };
                eventBus.RegisterEvent(stopMoveRight);
                break;

            default:
                break;
        }
    }


    public void RenderState() {
        container.blocks.RenderEntities();
        player.Render();
    }

    public void ResetState() {
        eventBus = BreakoutBus.GetBus();
        
        levelName = "level1"; // should be changed so you can change levels dynamically.
        levelPath = Path.Combine("Assets", "Levels", levelName + ".txt");
        levelData = levelLoader.ReadLevelFile(levelPath);

        //if (levelData == null) {
        //    Console.Error.WriteLine($"Error reading level file, please read above error message. Aborting run...");
        //    GameEvent exitGame = new GameEvent {
        //        EventType = GameEventType.WindowEvent,
        //        Message = "CLOSE_WINDOW"
        //    };
        //    eventBus.RegisterEvent(exitGame);
        //    return;
        //}
        container.CreateBlocks(levelData);

        player = new Player(
            new DynamicShape(new Vec2F(0.4f, 0.025f), new Vec2F(0.2f, 0.025f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));
        eventBus.Subscribe(GameEventType.MovementEvent, player);
    }

    public void UpdateState() {
        player.Move();
    }
}