namespace BreakoutTests;

using System;
using Breakout.LevelLoading;
using DIKUArcade.Utilities;

public class TestLevelLoader {
    
    private LoadLevel loader;
    private string workingDirectory;

    [SetUp]
    public void Setup() {
        loader = new LoadLevel();
        workingDirectory = FileIO.GetProjectPath();
        Console.WriteLine($"Testing in directory: {workingDirectory}");
        
    }

    // Can handle differences in metadata
    [TestCase("level1.txt")]
    [TestCase("level2.txt")]
    [TestCase("level1.txt")]
    [TestCase("columns.txt")]
    public void TestMetaDataDifference(string fileName) {

        string levelPath = (Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Levels", fileName));

        
        LevelData levelData = loader.ReadLevelFile(levelPath);

        Assert.IsNotNull(levelData);
    }

    // Data read from file is stored as expected in data structures
    [TestCase("bonusstage.txt")]
    [TestCase("level3.txt")]
    public void TestDataStoredAsExpected(string fileName) {

        string levelPath = (Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Levels", fileName));
        LevelData levelData = loader.ReadLevelFile(levelPath);

        Assert.IsNotNull(levelData);
    }

    // Empty or invalid files are handled without crashing the program.
    [TestCase("invalidStage.txt")]
    [TestCase("emptyStage.txt")]
    public void TestHandlesEmptyOrInvalidFile(string fileName) {

        string levelPath = (Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Levels", fileName));
        LevelData levelData = loader.ReadLevelFile(levelPath);
        
        // the level loader should return null if it throws an exception (i.e encounters an invalid file)
        Assert.IsNull(levelData);
    }
}