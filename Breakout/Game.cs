namespace Breakout;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using LevelLoading;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class Game : DIKUGame, IGameEventProcessor {
    private BlockContainer container = new BlockContainer();
    private LoadLevel loader = new LoadLevel();
    private LevelData levelData;
    private string levelName;
    private string levelPath;
    public Player player;
    private GameEventBus eventBus;



    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        levelName = "level1"; // set which level to use here

        levelPath = Path.Combine("Assets", "Levels", levelName + ".txt");
        levelData = loader.ReadLevelFile(levelPath);
        if (levelData == null) {
            Console.Error.WriteLine($"Error reading level file, please read above error message. Aborting run...");
            window.CloseWindow();
            return;
        }
        container.CreateBlocks(levelData);

        eventBus = GalagaBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType>
        { GameEventType.InputEvent, GameEventType.WindowEvent,
         GameEventType.MovementEvent, GameEventType.GameStateEvent });
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);
        player = new Player(
            new DynamicShape(new Vec2F(0.4f, 0.025f), new Vec2F(0.2f, 0.025f)),
            new Image(Path.Combine("Assets", "Images", "player.png")));
        eventBus.Subscribe(GameEventType.MovementEvent, player);

    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        this.HandleKeyEvent(action, key);
        if (action != KeyboardAction.KeyPress) {
            return;
        }
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;

            case KeyboardKey.Space:
                //Console.WriteLine(levelData.ToString());
                container.blocks.Iterate(block => {
                    block.Hit();
                });


                //loader.printLevelDataToConsole(loader.ReadLevelFile(levelPath));
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

    public override void Render() {
        container.blocks.RenderEntities();
        player.Render();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
    }

    public void ProcessEvent(GameEvent gameEvent) {
    }
}