namespace Breakout.LevelLoading;

using System.Text;

public class LevelParser {

    private string fileContent;

    private string mapSection, metaSection, legendSection;

    private int mapIndex, metaIndex, legendIndex;
    private int mapEndIndex, metaEndIndex, legendEndIndex;

    private string[] metaLines;
    private string[] legendLines;

    Dictionary<string, string> metaDict;
    Dictionary<string, string> legendDict;

    private FileHandler fileHandler;

    public LevelData ParseLevel(string fileName) {
        metaDict = new Dictionary<string, string>();
        legendDict = new Dictionary<string, string>();

        fileHandler = new FileHandler();
        fileContent = fileHandler.ReadFile(fileName);        
        
        // split whole string by indexes (map, meta, legend)
        mapIndex = fileContent.IndexOf("Map:");
        metaIndex = fileContent.IndexOf("Meta:");
        legendIndex = fileContent.IndexOf("Legend:");

        mapEndIndex = fileContent.IndexOf("Map/");
        metaEndIndex = fileContent.IndexOf("Meta/");
        legendEndIndex = fileContent.IndexOf("Legend/");


        if (mapIndex == -1 || metaIndex == -1 || legendIndex == -1 ||
            mapEndIndex == -1 || metaEndIndex == -1 || legendEndIndex == -1) {
                throw new InvalidDataException("Error: file is invalid");
            }

        mapSection = fileContent.Substring(mapIndex + "Map:".Length, mapEndIndex - (mapIndex + "Map:".Length)).Trim();
        metaSection = fileContent.Substring(metaIndex + "Meta:".Length, metaEndIndex - (metaIndex + "Meta/".Length)).Trim();
        legendSection = fileContent.Substring(legendIndex + "Legend:".Length, legendEndIndex - (legendIndex + "Legend:".Length)).Trim();

        metaLines = metaSection.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        legendLines = legendSection.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in metaLines) {
            string[] parts = line.Split(':'); //split by delimiter :

            if (parts.Length == 2) {
                string descriptor = parts[0].Trim();
                string value = parts[1].Trim();

                if (!metaDict.TryAdd(descriptor, value)) {
                    throw new InvalidOperationException($"Duplicate key '{descriptor}' found or meta section is otherwise invalid");
                }
            }
        }
        
        foreach (string line in legendLines) {
            string[] parts = line.Split(')'); //split by delimiter :

            if (parts.Length == 2) {
                string legend = parts[0].Trim();
                string asset = parts[1].Trim();

                if (!legendDict.TryAdd(legend, asset)) {
                    throw new InvalidOperationException($"Duplicate key '{legend}' found or legend section is otherwise invalid");
                }
            }
        }

        LevelData levelData = new LevelData {
                MapSection = mapSection,
                MetaDictionary = metaDict,
                LegendDictionary = legendDict,
        };

        return levelData;
    }
}