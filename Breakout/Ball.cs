namespace Breakout;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using LevelLoading;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using DIKUArcade.Math;

public class Ball : Entity
{
    public Entity entity;
    public DynamicShape shape;
    public bool movementSwitch;

    Vec2F ballSpeed = new Vec2F(0.01f, 0.01f);

    public Ball(DynamicShape shape, IBaseImage image) : base(shape, image)
    {
        entity = new Entity(shape, image);
        this.shape = shape;
        movementSwitch = false;
    }
    public void UpdateDirection()
    {
        if (movementSwitch == true && shape.Direction.X == 0.0f && shape.Direction.Y == 0.0f)
        {
            movementSwitch = true;
            shape.Direction.X = ballSpeed.X;
            shape.Direction.Y = ballSpeed.Y;
        }
    }

    public void Render()
    {
        RenderEntity();
    }

    public void ReverseXDirection()
    {
        shape.Direction.X *= -1;
    }
    public void ReverseYDirection()
    {
        shape.Direction.Y *= -1;
    }

    public void BounceFromPlayer(Player player)
    {
        float distanceFromCenter = this.shape.Position.X - player.shape().Position.X;
        float normalizedDistance = distanceFromCenter / (player.shape().Extent.X / 2.0f);
        shape.Direction.X += normalizedDistance / 100;
        shape.Direction.Y *= -1.0f;
    }



    public void Move()
    {
        shape.Move();

        if (shape.Position.Y < 0.0)
        {
            DeleteEntity();
            Delete
        }

        if (shape.Position.X > 0.95f || shape.Position.X < 0.00f)
        {
            ReverseXDirection();
        }

        if (shape.Position.Y > 0.95f)
        {
            ReverseYDirection();
        }
    }
    public Vec2F GetPosition() => shape.Position;

}