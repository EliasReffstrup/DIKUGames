namespace Breakout;

using System;
using DIKUArcade.GUI;

class Program
{
    static void Main(string[] args)
    {
        var windowArgs = new WindowArgs()
        {
            Title = "Breakout v0.1",
            Height = 500U,
            Width = 552U
        };

        var game = new Game(windowArgs);
        game.Run();
    }
}

