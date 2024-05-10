namespace BreakoutTests;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using System;
using Breakout;
using DIKUArcade.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using Breakout.LevelLoading;
using Breakout;

public class BallTests {
    private Ball ball;
    private Player player;
    public EntityContainer<Ball> ballsContainer = new EntityContainer<Ball>(100);

    [SetUp]
    public void Setup() {
    
    Window.CreateOpenGLContext();
    player = new Player(
        new DynamicShape(new Vec2F(0.4f, 0.025f), new Vec2F(0.2f, 0.025f)),
        new Image(Path.Combine("Assets", "Images", "player.png")));
    ball = new Ball(
        new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.05f, 0.05f)),
        new Image(Path.Combine("Assets", "Images", "ball.png")));
        ballsContainer.AddEntity(ball);
    }
    

    [Test]
    public void TestUpdateDirectionTest() {
        ball.movementSwitch = true;
        ball.UpdateDirection();
        Assert.AreEqual(0.01f, ball.shape.Direction.X, 0.001f);
        Assert.AreEqual(0.01f, ball.shape.Direction.Y, 0.001f);
    }


    [Test]
    public void BallGetsDeletedTest() {
        ball.shape.Position = new Vec2F(0.0f, -0.1f);
        ball.Move();
        Assert.IsTrue(ball.entity.IsDeleted());
    }
    
    [Test]
    public void TestBallCollidesWithBlock()
    {
        Block block = new Block(
            new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "darkgreen-block.png")),
            1,
            "test"
        );

        Ball ball = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.55f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "ball.png"))
        );

        ball.Move();
        block.Hit();
        Assert.AreEqual(0, block.health);
    }



    [Test]
    public void TestBallLaunchesUpwards()
    {
        ball.movementSwitch = true;
        ball.UpdateDirection();
        Assert.Greater(ball.GetPosition().Y, 0.5f);
    }

    [Test]
    public void TestAllBallsMoveAtSameSpeed()
    {
        Ball ball1 = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "ball.png"))
        );

        Ball ball2 = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "ball.png"))
        );

        ball1.movementSwitch = true;
        ball2.movementSwitch = true;
        ball1.UpdateDirection();
        ball2.UpdateDirection();

        Assert.AreEqual(ball1.GetPosition().X, ball2.GetPosition().X,0.001f);
        Assert.AreEqual(ball1.GetPosition().Y, ball2.GetPosition().Y,0.001f);
        }
}