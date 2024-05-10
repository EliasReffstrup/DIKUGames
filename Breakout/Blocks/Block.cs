namespace Breakout;
using System;
using System.Drawing.Printing;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Block : Entity {
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    private Entity entity;
    private StationaryShape shape;
    private int health;
    private string type;
    private string name;

    public Block(StationaryShape shape, Image image, int health, string type, string name = "") : base(shape, image) {
        entity = new Entity(shape, image);
        this.shape = shape;
        this.health = health;
        this.type = type;
        this.name = name;
        if (type == "Hardened") {
            this.health *= 2;
        }
    }

    public Vec2F Position {
        get => shape.Position;
    }

    public void Hit() {
        if (type == "Hardened") {
            Image = new Image(Path.Combine(workingDirectory,"..", "Breakout", "Assets", "Images", name + "-damaged.png"));
        }
        if (type == "Unbreakable") {
            health += 1;
        }
        health -= 1;
        if (health < 1) {
            this.DeleteEntity();
        }
    }
}