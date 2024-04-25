namespace Breakout;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public class Game : DIKUGame
{
    private BlockContainer container = new BlockContainer();



    public Game(WindowArgs windowArgs) : base(windowArgs)
    {
        window.SetKeyEventHandler(KeyHandler);
        container.CreateBlocks();
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key)
    {
        if (action != KeyboardAction.KeyPress)
        {
            return;
        }
        switch (key)
        {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
        }
    }

    public override void Render()
    {
        container.Blocks.RenderEntities();
    }

    public override void Update()
    {

    }
}