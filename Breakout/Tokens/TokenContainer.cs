namespace Breakout;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

public class TokenContainer : IGameEventProcessor
{
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    public EntityContainer<Token> tokens = new EntityContainer<Token>(255);

    private Dictionary<string, string> filenames = new Dictionary<string, string>() {
                                                                   { "Wide", "WidePowerUp.png" },
                                                                   { "Slim", "SlimJim.png" },
                                                                   { "Speed", "SpeedPickUp.png"},
                                                                   { "Slow", "Slowness.png"},
                                                                   {"Fire", "DamagePickUp.png"} };
    public void CreateToken(Vec2F pos, string name)
    {
        tokens.AddEntity(new Token(
                    new DynamicShape(pos,
                    new Vec2F(0.05f, 0.05f)),
                new Image(Path.Combine(workingDirectory, "..", "Breakout",
                "Assets", "Images", filenames[name])), name));
    }


    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.StatusEvent)
        {
            Vec2F pos = new Vec2F(0.0f, 0.0f);
            pos.X = (float)gameEvent.IntArg1 / 100;
            pos.Y = (float)gameEvent.Id / 100;
            pos.X += 0.025f;
            CreateToken(pos, gameEvent.Message);

        }
    }

}