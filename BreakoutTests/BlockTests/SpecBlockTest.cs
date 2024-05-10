namespace BreakoutTests;

using System;
using Breakout;
using DIKUArcade.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using Breakout.LevelLoading;


public class TestSpecBlocks {

    private BlockContainer container;
    private LoadLevel loader;
    private string workingDirectory;

    [OneTimeSetUp]
    public void Init() {
        Window.CreateOpenGLContext(); // We need a window to handle everything
    }

    [SetUp]
    public void Setup() {
        container = new BlockContainer();
        loader = new LoadLevel();
        workingDirectory = FileIO.GetProjectPath();
        Console.WriteLine($"Testing in directory: {workingDirectory}");

    }

    // Test that normal block dies after one hit
    [TestCase("blue-block.png")]
    [TestCase("green-block.png")]
    [TestCase("orange-block.png")]
    public void TestBlockDeletion(string fileName) {
        string assetPath = Path.Combine(workingDirectory, "..", "Breakout", "Assets",
        "Images", fileName);
        Block testBlock = new Block(
                    new StationaryShape(new Vec2F(1f, 1f),
                    new Vec2F(1f, 1f)),
                new Image(assetPath),
                0,
                ""
                );
        testBlock.Hit();
        Assert.IsTrue(testBlock.IsDeleted());
    }

    // Test that hardened blocks die after two hits
    [TestCase("blue-block.png")]
    [TestCase("green-block.png")]
    [TestCase("orange-block.png")]
    public void TestHardBlocks(string fileName) {
        string assetPath = Path.Combine(workingDirectory, "..", "Breakout", "Assets",
        "Images", fileName);
        Block testBlockA = new Block(
                    new StationaryShape(new Vec2F(1f, 1f),
                    new Vec2F(1f, 1f)),
                new Image(assetPath),
                1,
                "Hardened",
                assetPath.Substring(0, assetPath.Length - 4)
                );
        Console.WriteLine(assetPath);
        Block testBlockB = new Block(
                    new StationaryShape(new Vec2F(1f, 1f),
                    new Vec2F(1f, 1f)),
                new Image(assetPath),
                1,
                "Hardened",
                assetPath.Substring(0, assetPath.Length - 4)
                );
        testBlockA.Hit();
        testBlockA.Hit();
        testBlockB.Hit();
        Assert.Multiple(() => {
            Assert.IsTrue(testBlockA.IsDeleted());
            Assert.IsFalse(testBlockB.IsDeleted());
        });

    }

    //Test that unbreakable blocks don't ever die
    [Test]
    public void TestUnbreakBlocks() {
        string assetPath = Path.Combine(workingDirectory, "..", "Breakout", "Assets",
        "Images", "grey-block.png");
        Block testBlockA = new Block(
                    new StationaryShape(new Vec2F(1f, 1f),
                    new Vec2F(1f, 1f)),
                new Image(assetPath),
                1,
                "Unbreakable"
                );
        Console.WriteLine(assetPath);
        for (int i = 0; i < 100; i++) {
            testBlockA.Hit();
        }
        Assert.Multiple(() => {
            Assert.IsFalse(testBlockA.IsDeleted());
        });

    }

}