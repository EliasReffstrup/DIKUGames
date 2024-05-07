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

    public override string ToString() {
        if (this != null) {
            // Format the level data as a string
            string mapSectionString = $"\nPrinting Map:\n{MapSection}\n\nLength of map: {MapSection.Length}\n";

            string metaSectionString = "\nPrinting Meta:\n";
            foreach (var kvp in MetaDictionary) {
                metaSectionString += $"{kvp.Key}: {kvp.Value}\n";
            }

            string legendSectionString = "\nPrinting Legend:\n";
            foreach (var kvp in LegendDictionary) {
                legendSectionString += $"{kvp.Key}) {kvp.Value}\n";
            }

            return mapSectionString + metaSectionString + legendSectionString;
        } else {
            return "No level data provided";
        }
    }
}