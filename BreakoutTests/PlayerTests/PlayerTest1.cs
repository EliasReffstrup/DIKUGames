namespace BreakoutTests;

using System.IO;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using NUnit.Framework;
using DIKUArcade.Events;
using DIKUArcade.Utilities;

public class TestPlayer {
    Player player;
    GameEventBus eventBus;
    private string workingDirectory;

    [SetUp]
    public void Setup() { 
        workingDirectory = FileIO.GetProjectPath();

        player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.025f), new Vec2F(0.2f, 0.025f)),
                new Image(Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Images", "player.png")));
        eventBus = BreakoutBus.GetBus();
        eventBus.Subscribe(GameEventType.MovementEvent, player);
    }

    [Test]
    public void TestPlayerStartsInHorizontalCenter()
    {
        Assert.AreEqual(0.4f, player.GetPosition().X);
    }

    [Test]
    public void TestPlayerInitialPositionInsideBoundaries() {
       
        Assert.True(player.GetPosition().X > 0.0f && 
                    player.GetPosition().X < 1.0f &&
                    player.GetPosition().Y > 0.0f &&
                    player.GetPosition().Y < 1.0f);

    }
    [Test]
    public void TestPlayerCanMoveHorizontally(){
        player.SetMoveLeft(true);
        player.Move();
        Assert.AreEqual(0.39f, player.GetPosition().X, 0.1);

        player.SetMoveRight(true);
        player.Move();
        Assert.AreEqual(0.4f, player.GetPosition().X, 0.1f);
    }

    [Test]
    public void TestPlayerCannotMoveOutOfLeftBoundary() {
        for (int i = 0; i < 100; i++) {
            player.SetMoveLeft(true);
            player.Move();
        }

        Assert.True(player.GetPosition().X >= 0.0f && 
                    player.GetPosition().X <= 1.0f);
    }
    [Test]
    public void TestPlayerCannotMoveOutOfRightBoundary() {
        for (int i = 0; i < 100; i++) {
            player.SetMoveRight(true);
            player.Move();
        }

        Assert.True(player.GetPosition().X >= 0.0f && 
                    player.GetPosition().X <= 1.0f);
    }

    [Test]
    public void TestPlayerExistsInBottomHalfOfScreen()
    {
        Assert.IsTrue(player.GetPosition().Y < 0.5f);
    }
}