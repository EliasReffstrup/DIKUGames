namespace Breakout;
using System;
using System.Drawing.Printing;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Token : Entity
{
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    private Entity entity;
    public DynamicShape shape;
    public string name;

    public Token(DynamicShape shape, Image image, string name)
    : base(shape, image)
    {
        entity = new Entity(shape, image);
        this.shape = shape;
        this.name = name;
        this.shape.Direction.Y = -0.01f;
    }

    public Vec2F Position
    {
        get => shape.Position;
    }
    public void Move()
    {
        Shape.Position.Y -= 0.01f;
    }
}