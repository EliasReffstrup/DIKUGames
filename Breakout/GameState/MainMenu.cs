namespace Breakout.GameState;

using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class MainMenu : IGameState {
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    private static MainMenu instance = null;
    private Entity backGroundImage;
    private Text[] menuButtons = new Text[3];
    private int activeMenuButton = 0;
    private int maxMenuButtons = 3;

    public static MainMenu GetInstance() {
        if (instance == null) {
            MainMenu.instance = new MainMenu();
            MainMenu.instance.ResetState();
        }
        return MainMenu.instance;
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease) {
            switch (key) {
                case KeyboardKey.Down:
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
                    activeMenuButton++;
                    if (activeMenuButton >= maxMenuButtons)
                        activeMenuButton = 0;
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.HotPink);
                    break;
                case KeyboardKey.Up:
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.White);
                    activeMenuButton--;
                    if (activeMenuButton < 0)
                        activeMenuButton = maxMenuButtons - 1;
                    menuButtons[activeMenuButton].SetColor(System.Drawing.Color.HotPink);
                    break;
                case KeyboardKey.Enter:
                    switch (activeMenuButton) {
                        case 0:
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING",
                                StringArg2 = "RESET"
                            });
                            GameRunning.GetInstance().ResetLevel();
                            break;
                        case 1:
                            Console.WriteLine("Not currently implemented");
                            break;
                        case 2:
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                Message = "CLOSE_WINDOW"
                            });
                            break;
                        default:
                            instance = null;
                            BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING",
                                StringArg2 = "RESET"
                            });
                            
                            break;
                    }
                    break;
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        Message = "CLOSE_WINDOW"
                    });
                    break;

                default:
                    break;
            }
        }
    }

    public void RenderState() {
        backGroundImage.RenderEntity();
        for (int i = 0; i < menuButtons.Length; i++) {
            menuButtons[i].RenderText();
        }
    }

    public void ResetState() {
        backGroundImage = new Entity(new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine(workingDirectory,"..", "Breakout", "Assets", "Images", "shipit_titlescreen.png")));

        menuButtons[0] = new("[NEW GAME]", new Vec2F(0.1f, 0.28f), new Vec2F(0.4f, 0.4f));
        menuButtons[0].SetColor(System.Drawing.Color.HotPink);
        menuButtons[1] = new("[LVL SELECT]", new Vec2F(0.1f, 0.18f), new Vec2F(0.4f, 0.4f));
        menuButtons[1].SetColor(System.Drawing.Color.White);
        menuButtons[2] = new("[QUIT]", new Vec2F(0.1f, 0.008f), new Vec2F(0.4f, 0.4f));
        menuButtons[2].SetColor(System.Drawing.Color.White);
    }

    public void UpdateState() {
    }
}