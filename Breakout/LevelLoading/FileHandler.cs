namespace Breakout.LevelLoading;
/// <summary>Class for the StateMachine for when the game is over.</summary>
public class FileHandler
{
    public string ReadFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException("Error! File not found:" + fileName);
        }
        return File.ReadAllText(fileName);
    }
}