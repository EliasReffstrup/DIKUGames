namespace Breakout;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class BlockContainer
{
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    public EntityContainer<Block> blocks = new EntityContainer<Block>(288);
    private Random rand = new Random();
    private string[] powerTypes = { "Wide", "Slim", "Speed", "Slow", "Fire" };
    public void CreateBlocks(LevelData data)
    {
        for (int j = 0; j < 24; j++)
        {
            for (int i = 0; i < 12; i++)
            {
                var type = "";
                var name = "";
                var powerup = "";
                char currentLetter = data.MapSection[j * 14 + i];
                if (currentLetter != '-')
                {
                    if (data.MetaDictionary.ContainsKey("Hardened") &&
                    data.MetaDictionary["Hardened"] == currentLetter.ToString())
                    {
                        type = "Hardened";
                        name = Path.Combine("Assets", "Images",
                        data.LegendDictionary[currentLetter.ToString()].Substring
                        (0, data.LegendDictionary[currentLetter.ToString()].Length - 4));
                    }
                    if (data.MetaDictionary.ContainsKey("Unbreakable") &&
                    data.MetaDictionary["Unbreakable"] == currentLetter.ToString())
                    {
                        type = "Unbreakable";
                    }
                    if (data.MetaDictionary.ContainsKey("PowerUp") &&
                    data.MetaDictionary["PowerUp"] == currentLetter.ToString())
                    {
                        powerup = powerTypes[rand.Next(powerTypes.Length)];
                    }
                    blocks.AddEntity(new Block(
                    new StationaryShape(new Vec2F(0.0835f * i, 0.971f - 0.0278f * j),
                    new Vec2F(0.0835f, 0.0278f)),
                new Image(Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Images",
                data.LegendDictionary[currentLetter.ToString()])),
                1,
                type,
                name,
                powerup
                ));
                }
            }
        }
    }

    //Alternate block creation function for when testing from a different folder. workingDirectory
    //must be a path to the images folder.
    public void CreateBlocks(LevelData data, string workingDirectory)
    {
        for (int j = 0; j < 24; j++)
        {
            for (int i = 0; i < 12; i++)
            {
                var type = "";
                var name = "";
                var powerup = "";
                char currentLetter = data.MapSection[j * 14 + i];
                if (currentLetter != '-')
                {
                    if (data.MetaDictionary.ContainsKey("Hardened") &&
                    data.MetaDictionary["Hardened"] == currentLetter.ToString())
                    {
                        type = "Hardened";
                        name = Path.Combine(workingDirectory,
                        data.LegendDictionary[currentLetter.ToString()].Substring
                        (0, data.LegendDictionary[currentLetter.ToString()].Length - 4));
                    }
                    if (data.MetaDictionary.ContainsKey("Unbreakable") &&
                    data.MetaDictionary["Unbreakable"] == currentLetter.ToString())
                    {
                        type = "Unbreakable";
                    }
                    blocks.AddEntity(new Block(
                    new StationaryShape(new Vec2F(0.0835f * i, 0.971f - 0.0278f * j),
                    new Vec2F(0.0835f, 0.0278f)),
                new Image(Path.Combine(workingDirectory,
                data.LegendDictionary[currentLetter.ToString()])),
                1,
                type,
                name,
                powerup
                ));
                }
            }
        }
    }
}