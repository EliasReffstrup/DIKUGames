namespace Breakout;
using DIKUArcade.Events;

/// <summary>The eventBus that handles all the in-game events.</summary>

public static class BreakoutBus
{
    private static GameEventBus eventBus;

    public static GameEventBus GetBus()
    {
        return BreakoutBus.eventBus ?? (BreakoutBus.eventBus =
                                        new GameEventBus());
    }
}