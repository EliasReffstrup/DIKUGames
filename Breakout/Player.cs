namespace Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class Player : IGameEventProcessor {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    public const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }

    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        shape.Move();
        if (shape.Position.X < 0.0f) {
            shape.Position.X = 0.0f;
        } else if (shape.Position.X > 0.8f) {
            shape.Position.X = 0.8f;
        }
    }

    private void SetMoveLeft(bool val) {
        moveLeft = val ? -MOVEMENT_SPEED : 0.0f;
        UpdateDirection();

    }

    private void SetMoveRight(bool val) {
        moveRight = val ? MOVEMENT_SPEED : 0.0f;
        UpdateDirection();
    }

    public Vec2F GetPosition() => shape.Position;

    public void ProcessEvent(GameEvent gameEvent) {

        if (gameEvent.EventType == GameEventType.MovementEvent) {
            switch (gameEvent.Message) {
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
}