namespace Breakout.LevelLoading;

public class FileHandler {
    public string ReadFile(string fileName) {
        if (!File.Exists(fileName)) {
            throw new FileNotFoundException("Error! File not found:" + fileName);
        }
        return File.ReadAllText(fileName);
    }
}