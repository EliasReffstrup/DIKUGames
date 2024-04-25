namespace Breakout;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class BlockContainer
{
    public EntityContainer<Block> Blocks = new EntityContainer<Block>(288);
    public void CreateBlocks()
    {
        Blocks.AddEntity(new Block(
            new StationaryShape(new Vec2F(0.5f, 0.5f), new Vec2F(0.09f, 0.03f)),
        new Image(Path.Combine("Assets", "Images", "blue-block.png")),
        0,
        ""
        ));
    }
}