namespace Breakout.LevelLoading;
using System.IO;

public class LoadLevel {

    string fileContent;

    string mapSection, metaSection, legendSection;

    int mapIndex, metaIndex, legendIndex;
    int mapEndIndex, metaEndIndex, legendEndIndex;

    string[] metaLines;
    string[] legendLines;

    Dictionary<string, string> metaDict;
    Dictionary<string, string> legendDict;

    public LevelData ReadLevelFile(string fileName) {

        metaDict = new Dictionary<string, string>();
        legendDict = new Dictionary<string, string>();

        try {
            // check if file exists
            if (!File.Exists(fileName)) {
                throw new FileNotFoundException($"File not found: '{fileName}'");
            }

            fileContent = File.ReadAllText(fileName);

            // split whole string by indexes (map, meta, legend)
            mapIndex = fileContent.IndexOf("Map:");
            metaIndex = fileContent.IndexOf("Meta:");
            legendIndex = fileContent.IndexOf("Legend:");

            mapEndIndex = fileContent.IndexOf("Map/");
            metaEndIndex = fileContent.IndexOf("Meta/");
            legendEndIndex = fileContent.IndexOf("Legend/");


            // check if file contains required indexes to be a map file before doing any processing
            if (mapIndex == -1 || metaIndex == -1 || legendIndex == -1 ||
                mapEndIndex == -1 || metaEndIndex == -1 || legendEndIndex == -1) {

                throw new InvalidDataException("Level data is empty or invalid");
            }

            // create substrings from indexes
            mapSection = fileContent.Substring(mapIndex + "Map:".Length, mapEndIndex - (mapIndex + "Map:".Length)).Trim();
            metaSection = fileContent.Substring(metaIndex + "Meta:".Length, metaEndIndex - (metaIndex + "Meta/".Length)).Trim();
            legendSection = fileContent.Substring(legendIndex + "Legend:".Length, legendEndIndex - (legendIndex + "Legend:".Length)).Trim();

            if (mapSection.Length < 300 || mapSection.Length > 400) {
                throw new InvalidDataException("Map section is empty");
            }

            // further divide meta and legend into substrings, map can stay as is
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

            // add data to ADT so it can be returned
            LevelData levelData = new LevelData {
                MapSection = mapSection,
                MetaDictionary = metaDict,
                LegendDictionary = legendDict,
            };

            // return the 2 dictionaries and map
            return levelData;

            // catch all the possible errors.. so many possible errors...
            // file not found, invalid data, invalid operation
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

    public void printLevelDataToConsole(LevelData levelData) {
        if (levelData != null) {
            Console.WriteLine("\nPrinting Map:\n");
            Console.WriteLine(levelData.MapSection);
            Console.WriteLine($"\nLength of map: {mapSection.Length}");

            Console.WriteLine("\nPrinting Meta:\n");
            foreach (var kvp in levelData.MetaDictionary) {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            Console.WriteLine("\nPrinting Legend:\n");
            foreach (var kvp in levelData.LegendDictionary) {
                Console.WriteLine($"{kvp.Key}) {kvp.Value}");
            }
        } else {
            Console.WriteLine("No level data provided");
        }
    }
}