namespace BreakoutTests;

using System;
using Breakout;
using DIKUArcade.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.GUI;
using Breakout.LevelLoading;


public class TestBlocks {

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

    // Testing that blocks can be generated successfully
    [TestCase("blue-block.png")]
    [TestCase("green-block.png")]
    [TestCase("orange-block.png")]
    public void TestBlockGeneration(string fileName) {
        string assetPath = Path.Combine(workingDirectory, "..", "Breakout", "Assets",
        "Images", fileName);
        Block testBlock = new Block(
                    new StationaryShape(new Vec2F(1f, 1f),
                    new Vec2F(1f, 1f)),
                new Image(assetPath),
                0,
                ""
                );
        Console.WriteLine(assetPath);
        Assert.IsNotNull(testBlock);
    }

    // Testing that the generator works with the level loader.
    [TestCase("level1.txt")]
    [TestCase("level2.txt")]
    [TestCase("level3.txt")]
    public void TestBlockContainerLevelLoading(string fileName) {
        string levelPath = Path.Combine(workingDirectory, "..", "Breakout", "Assets",
        "Levels", fileName);
        string imagePath = Path.Combine(workingDirectory, "..", "Breakout", "Assets",
        "Images");
        LevelData levelData = loader.ReadLevelFile(levelPath);
        container.CreateBlocks(levelData, imagePath);
        Assert.IsNotNull(levelData);

    }
}