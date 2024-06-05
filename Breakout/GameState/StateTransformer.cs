namespace Breakout.GameState;

public class StateTransformer {
    static public GameStateType TransformStringToState(string state) {
        switch (state) {
            case "GAME_RUNNING":
                return GameStateType.GameRunning;
            case "GAME_PAUSED":
                return GameStateType.GamePaused;
            case "MAIN_MENU":
                return GameStateType.MainMenu;
            case "GAME_OVER":
            return GameStateType.GameOver;
            case "GAME_WON":
            return GameStateType.GameWon;
            default:
                throw new ArgumentException("INVALID STATE");
        }
    }
    static public string TransformStateToString(GameStateType state) {
        switch (state) {
            case GameStateType.GameRunning:
                return "GAME_RUNNING";
            case GameStateType.GamePaused:
                return "GAME_PAUSED";
            case GameStateType.MainMenu:
                return "MAIN_MENU";
            case GameStateType.GameOver:
                return "GAME_OVER";
            case GameStateType.GameWon:
                return "GAME_WON";
            default:
                throw new ArgumentException("INVALID STATE");
        }
    }
}