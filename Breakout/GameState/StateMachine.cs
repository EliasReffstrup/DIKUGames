namespace Breakout.GameState;

using DIKUArcade.Events;
using DIKUArcade.State;
/// <summary>Handles the different game states.</summary>
public class StateMachine : IGameEventProcessor
{
    public IGameState ActiveState { get; private set; }

    public StateMachine()
    {
        BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
        // GameRunning.GetInstance();
        // GamePaused.GetInstance();
    }

    private void SwitchState(GameStateType stateType)
    {
        switch (stateType)
        {
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            case GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case GameStateType.GameRunning:
                ActiveState = GameRunning.GetInstance();
                break;
            case GameStateType.GameOver:
                ActiveState = GameOver.GetInstance();
                break;
            case GameStateType.GameWon:
                ActiveState = GameWon.GetInstance();
                break;
            default:
                throw new ArgumentException("No valid state");
        }
    }

    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.GameStateEvent)
        {
            switch (gameEvent.Message)
            {
                case "CHANGE_STATE":
                    GameStateType newState =
                    StateTransformer.TransformStringToState(gameEvent.StringArg1);
                    SwitchState(newState);
                    if (gameEvent.StringArg2 == "RESET")
                    {
                        ActiveState.ResetState();
                    }
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent
                        {
                            EventType = GameEventType.GameStateEvent,
                            Message = "UPDATE",
                        }
                    );

                    break;
            }
        }
    }
}