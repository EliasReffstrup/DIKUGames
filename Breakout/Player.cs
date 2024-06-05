namespace Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
/// <summary>Class for the player, storing all of it's values</summary>

public class Player : IGameEventProcessor
{
    private Entity entity;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    public const float MOVEMENT_SPEED = 0.013f;
    private float speedMod = 1.0f;
    private DynamicShape dynamicShape;
    private int lives = 2;

    public Player(DynamicShape shape, IBaseImage image)
    {
        entity = new Entity(shape, image);
        dynamicShape = shape;
    }
    private int[] counters = new int[3];
    public bool Fire = false;

    private void UpdateDirection()
    {
        shape().Direction.X = moveLeft + moveRight;
    }

    public void Render()
    {
        entity.RenderEntity();
    }

    public void Move()
    {
        shape().Move();
        if (shape().Position.X < 0.0f)
        {
            shape().Position.X = 0.0f;
        }
        else if (shape().Position.X > 0.8f)
        {
            shape().Position.X = 0.8f;
        }
        if (counters[0] > 0)
        {
            counters[0]--;
            if (counters[0] == 0)
            {
                if (shape().Extent.X == 0.3f)
                {
                    shape().Position.X += 0.05f;
                }
                else if (shape().Extent.X == 0.15f)
                {
                    shape().Position.X -= 0.025f;
                }
                shape().Extent.X = 0.2f;
            }
            if (counters[1] > 0)
            {
                counters[1]--;
                if (counters[1] == 0)
                {
                    speedMod = 1.0f;
                }
            }
            if (counters[2] > 0)
            {
                counters[2]--;
                if (counters[2] == 0)
                {
                    Fire = false;
                }
            }
        }
    }

    public void SetMoveLeft(bool val)
    {
        moveLeft = val ? (-MOVEMENT_SPEED * speedMod) : 0.0f;
        UpdateDirection();

    }

    public void SetMoveRight(bool val)
    {
        moveRight = val ? (MOVEMENT_SPEED * speedMod) : 0.0f;
        UpdateDirection();
    }

    public Vec2F GetPosition() => shape().Position;

    public DynamicShape shape() => dynamicShape;

    public void PowerUp(string type)
    {
        switch (type)
        {
            case "Wide":
                if (shape().Extent.X == 0.2f)
                {
                    shape().Position.X -= 0.05f;
                }
                else if (shape().Extent.X == 0.15f)
                {
                    shape().Position.X -= 0.075f;
                }
                shape().Extent.X = 0.3f;
                counters[0] = 600;
                break;
            case "Slim":
                if (shape().Extent.X == 0.2f)
                {
                    shape().Position.X += 0.025f;
                }
                else if (shape().Extent.X == 0.3f)
                {
                    shape().Position.X += 0.075f;
                }
                shape().Extent.X = 0.15f;
                counters[0] = 240;
                break;
            case "Speed":
                speedMod = 1.6f;
                counters[1] = 300;
                break;
            case "Slow":
                speedMod = 0.7f;
                counters[1] = 200;
                break;
            case "Fire":
                Fire = true;
                counters[2] = 120;
                break;
        }

    }

    public void ProcessEvent(GameEvent gameEvent)
    {

        if (gameEvent.EventType == GameEventType.MovementEvent)
        {
            switch (gameEvent.Message)
            {
                case "MOVE_LEFT":
                    SetMoveLeft(true);
                    break;

                case "MOVE_RIGHT":
                    SetMoveRight(true);
                    break;

                case "STOP_MOVE_LEFT":
                    SetMoveLeft(false);
                    break;

                case "STOP_MOVE_RIGHT":
                    SetMoveRight(false);
                    break;

                default:
                    break;
            }
        }
    }

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

}