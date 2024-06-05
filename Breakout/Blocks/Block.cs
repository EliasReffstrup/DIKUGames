namespace Breakout;
using System;
using System.Drawing.Printing;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
/// <summary>The block entity that makes up levels. Contained within the BlockContainer class</summary>
public class Block : Entity
{
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    private Entity entity;
    public StationaryShape shape;
    public int health;
    private string type;
    private string name;
    private string powerup = "";
    private GameEventBus eventBus;

    public Block(StationaryShape shape, Image image, int health, string type, string name = "", string powerup = "")
    : base(shape, image)
    {
        entity = new Entity(shape, image);
        this.shape = shape;
        this.health = health;
        this.type = type;
        this.name = name;
        this.powerup = powerup;
        if (type == "Hardened")
        {
            this.health *= 2;
        }
        eventBus = BreakoutBus.GetBus();
    }

    public Vec2F Position
    {
        get => shape.Position;
    }

    public void Hit()
    {
        if (type == "Hardened")
        {
            Image = new Image(Path.Combine(workingDirectory, "..", "Breakout", name + "-damaged.png"));
        }
        if (type == "Unbreakable")
        {
            health += 1;
        }
        health -= 1;
        if (health < 1)
        {
            if (powerup != "")
            {
                GameEvent sendPower = new GameEvent
                {
                    EventType = GameEventType.StatusEvent,
                    Message = powerup,
                    IntArg1 = (int)(Position.X * 100),
                    Id = (uint)(Position.Y * 100),
                };
                eventBus.RegisterEvent(sendPower);
            }
            DeleteEntity();
        }
    }
}