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
using DIKUArcade.State;
using Breakout.GameState;


public class Game : DIKUGame, IGameEventProcessor {
    // private BlockContainer container = new BlockContainer();
    // private LoadLevel loader = new LoadLevel();
    // private LevelData levelData;
    //private string levelName;
    //private string levelPath;
    // public Player player;
    private GameEventBus eventBus;

    private StateMachine stateMachine;
    private IGameState currentState;


    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        eventBus = BreakoutBus.GetBus();
        
        eventBus.InitializeEventBus(new List<GameEventType>
        { GameEventType.InputEvent, GameEventType.WindowEvent,
         GameEventType.MovementEvent, GameEventType.GameStateEvent });
        
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);

        stateMachine = new StateMachine();
        currentState = stateMachine.ActiveState;
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        currentState.HandleKeyEvent(action, key);

        // this.HandleKeyEvent(action, key);
        // if (action != KeyboardAction.KeyPress) {
        //     return;
        // }
        // switch (key) {
        //     case KeyboardKey.Escape:
        //         GameEvent exitGame = new GameEvent {
        //             EventType = GameEventType.WindowEvent,
        //             Message = "CLOSE_WINDOW"
        //         };
        //         eventBus.RegisterEvent(exitGame);
        //         break;

        //     case KeyboardKey.Space:
        //         //Console.WriteLine(levelData.ToString());
        //         container.blocks.Iterate(block => {
        //             block.Hit();
        //         });
        //         break;
        // }
    }
    // private void KeyPress(KeyboardKey key) {
    //     switch (key) {
    //         case KeyboardKey.Left:
    //             GameEvent moveLeft = new GameEvent {
    //                 EventType = GameEventType.MovementEvent,
    //                 Message = "MOVE_LEFT"
    //             };
    //             eventBus.RegisterEvent(moveLeft);
    //             break;

    //         case KeyboardKey.Right:
    //             GameEvent moveRight = new GameEvent {
    //                 EventType = GameEventType.MovementEvent,
    //                 Message = "MOVE_RIGHT"
    //             };
    //             eventBus.RegisterEvent(moveRight);
    //             break;

    //         default:
    //             break;
    //     }
    // }

    // private void KeyRelease(KeyboardKey key) {
    //     switch (key) {
    //         case KeyboardKey.Left:
    //             GameEvent stopMoveLeft = new GameEvent {
    //                 EventType = GameEventType.MovementEvent,
    //                 Message = "STOP_MOVE_LEFT"
    //             };
    //             eventBus.RegisterEvent(stopMoveLeft);
    //             break;

    //         case KeyboardKey.Right:
    //             GameEvent stopMoveRight = new GameEvent {
    //                 EventType = GameEventType.MovementEvent,
    //                 Message = "STOP_MOVE_RIGHT"
    //             };
    //             eventBus.RegisterEvent(stopMoveRight);
    //             break;

    //         default:
    //             break;
    //     }
    // }

    // public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
    //     switch (action) {
    //         case KeyboardAction.KeyPress:
    //             KeyPress(key);
    //             break;

    //         case KeyboardAction.KeyRelease:
    //             KeyRelease(key);
    //             break;

    //         default:
    //             break;
    //     }
    // }

    public override void Render() {
        currentState.RenderState();
        //container.blocks.RenderEntities();
        //player.Render();
    }

    public override void Update() {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        currentState.UpdateState();
        // player.Move();
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                    break;
            }
        }else if (gameEvent.EventType == GameEventType.GameStateEvent && gameEvent.Message == "UPDATE") {
            currentState = stateMachine.ActiveState;
        }
    }
}