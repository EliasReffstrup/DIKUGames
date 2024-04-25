namespace Breakout.LevelLoading;

public class LevelData {
    public string MapSection {
        get; set;
    }
    public Dictionary<string, string> MetaDictionary {
        get; set;
    }
    public Dictionary<string, string> LegendDictionary {
        get; set;
    }
}