namespace Breakout;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Breakout.LevelLoading;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class BlockContainer {
    public EntityContainer<Block> blocks = new EntityContainer<Block>(288);
    public void CreateBlocks(LevelData data) {
        for (int j = 0; j < 24; j++) {
            for (int i = 0; i < 12; i++) {
                char currentLetter = data.MapSection[j * 14 + i];
                if (currentLetter != '-') {
                    blocks.AddEntity(new Block(
                    new StationaryShape(new Vec2F(0.0835f * i, 0.971f - 0.0278f * j),
                    new Vec2F(0.0835f, 0.0278f)),
                new Image(Path.Combine("Assets", "Images",
                data.LegendDictionary[currentLetter.ToString()])),
                0,
                ""
                ));
                }
            }
        }
    }

    //Alternate block creation function for when testing from a different folder. workingDirectory
    //must be a path to the images folder.
    public void CreateBlocks(LevelData data, string workingDirectory) {
        for (int j = 0; j < 24; j++) {
            for (int i = 0; i < 12; i++) {
                char currentLetter = data.MapSection[j * 14 + i];
                if (currentLetter != '-') {
                    blocks.AddEntity(new Block(
                    new StationaryShape(new Vec2F(0.0835f * i, 0.971f - 0.0278f * j),
                    new Vec2F(0.0835f, 0.0278f)),
                new Image(Path.Combine(workingDirectory,
                data.LegendDictionary[currentLetter.ToString()])),
                0,
                ""
                ));
                }
            }
        }
    }
}