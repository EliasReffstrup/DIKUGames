namespace Breakout;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using LevelLoading;

public class Game : DIKUGame {
    private BlockContainer container = new BlockContainer();
    private LoadLevel loader = new LoadLevel();


    public Game(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler(KeyHandler);
        container.CreateBlocks(loader.ReadLevelFile("bonusstage.txt"));
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
                loader.printLevelDataToConsole(loader.ReadLevelFile("level3.txt"));
                break;
        }
    }

    public override void Render() {
        container.Blocks.RenderEntities();
    }

    public override void Update() {

    }
}