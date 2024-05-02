namespace Breakout.LevelLoading;
using System.IO;

public class LoadLevel {

    private LevelParser levelParser;

    public LevelData ReadLevelFile(string fileName) {
        levelParser = new LevelParser();

        try {
            return levelParser.ParseLevel(fileName);
        } catch (FileNotFoundException e) {
            Console.Error.WriteLine($"Error reading level file; {e.Message}");
            return null;
        } catch (InvalidDataException e) {
            Console.Error.WriteLine($"Error reading level file; {e.Message}");
            return null;
        } catch (InvalidOperationException e) {
            Console.Error.WriteLine($"Error reading level file; {e.Message}");
            return null;
        }
    }
}