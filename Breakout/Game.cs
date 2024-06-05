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

/// <summary>The top-level class handling the entire game</summary>

public class Game : DIKUGame, IGameEventProcessor
{
    private GameEventBus eventBus;

    private StateMachine stateMachine;
    private IGameState currentState;

    public Game(WindowArgs windowArgs) : base(windowArgs)
    {
        window.SetKeyEventHandler(KeyHandler);

        eventBus = BreakoutBus.GetBus();

        eventBus.InitializeEventBus(new List<GameEventType>
        { GameEventType.InputEvent, GameEventType.WindowEvent,
         GameEventType.MovementEvent, GameEventType.GameStateEvent,
         GameEventType.StatusEvent });

        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.WindowEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, this);

        stateMachine = new StateMachine();
        currentState = stateMachine.ActiveState;
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key)
    {
        currentState.HandleKeyEvent(action, key);
    }

    public override void Render()
    {
        currentState.RenderState();
    }

    public override void Update()
    {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        currentState.UpdateState();
    }

    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.WindowEvent)
        {
            switch (gameEvent.Message)
            {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                    break;
            }
        }
        else if (gameEvent.EventType == GameEventType.GameStateEvent && gameEvent.Message == "UPDATE")
        {
            currentState = stateMachine.ActiveState;
        }
    }
}