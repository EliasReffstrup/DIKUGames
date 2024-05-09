namespace Breakout.GameState;

using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class GamePaused : IGameState {
    private static GamePaused instance = null;
    private Text[] menuButtons = new Text[2];
    private int activeMenuButton = 0;
    private int maxMenuButtons = 2;
    public static GamePaused GetInstance() {
        if (GamePaused.instance == null) {
            GamePaused.instance = new GamePaused();
            GamePaused.instance.ResetState();
        }

        return GamePaused.instance;
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease) {
            switch (key) {
                case KeyboardKey.Down:
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
                    activeMenuButton++;
                    if (activeMenuButton >= maxMenuButtons)
                        activeMenuButton = 0;
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.Yellow);
                    break;
                case KeyboardKey.Up:
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
                    activeMenuButton--;
                    if (activeMenuButton < 0)
                        activeMenuButton = maxMenuButtons - 1;
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.Yellow);
                    break;
                case KeyboardKey.Enter:
                    if (activeMenuButton == 1) {
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "MAIN_MENU",
                            StringArg2 = "RESET"
                        });
                    } else {
                        instance = null;
                        BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_RUNNING"
                        }
                        );
                    }
                    break;
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        Message = "CHANGE_STATE",
                        StringArg1 = "GAME_RUNNING"
                    });
                    break;

                default:
                    break;
            }
        }
    }

    public void RenderState() {
        for (int i = 0; i < menuButtons.Length; i++) {
            menuButtons[i].RenderText();
        }
    }

    public void ResetState() {
        menuButtons[0] = new("[CONTINUE]", new Vec2F(0.1f, 0.28f), new Vec2F(0.4f, 0.4f));
        menuButtons[0].SetColor(System.Drawing.Color.Yellow);
        menuButtons[1] = new("[MAIN MENU]", new Vec2F(0.1f, 0.18f), new Vec2F(0.4f, 0.4f));
        menuButtons[1].SetColor(System.Drawing.Color.White);


    }

    public void UpdateState() {
        
    }
}