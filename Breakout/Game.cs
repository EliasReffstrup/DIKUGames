namespace Breakout;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using LevelLoading;

public class Game : DIKUGame {
    private BlockContainer container = new BlockContainer();
    private LoadLevel loader = new LoadLevel();
    private LevelData levelData;
    private string levelName;
    private string levelPath;


    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);

        levelName = "columns"; // set which level to use here

        levelPath = Path.Combine("Assets", "Levels", levelName + ".txt");
        levelData = loader.ReadLevelFile(levelPath);
        if (levelData == null) {
            Console.WriteLine($"Error reading level file, please read above error message. Aborting run...");
            window.CloseWindow();
            return;
        }

        container.CreateBlocks(levelData);
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action != KeyboardAction.KeyPress) {
            return;
        }
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;

            case KeyboardKey.Space:
                loader.printLevelDataToConsole(loader.ReadLevelFile(levelPath));
                break;
        }
    }

    public override void Render() {
        container.Blocks.RenderEntities();
    }

    public override void Update() {

    }
}