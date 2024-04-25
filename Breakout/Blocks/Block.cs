namespace Breakout;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Block : Entity {
    private Entity entity;
    private StationaryShape shape;
    private int health;
    private string type;

    public Block(StationaryShape shape, Image image, int health, string type) : base(shape, image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        this.health = health;
        this.type = type;
    }

    public Vec2F Position {
        get => shape.Position;
    }
}