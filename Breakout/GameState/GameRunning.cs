namespace Breakout.GameState;

using System;
using DIKUArcade;
using DIKUArcade.GUI;
using Breakout;
using DIKUArcade.Input;
using LevelLoading;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.State;
using Breakout.GameState;
using Breakout;

public class GameRunning : IGameState
{
    private string workingDirectory = DIKUArcade.Utilities.FileIO.GetProjectPath(); // to make testing work

    private static GameRunning instance = null;
    private GameEventBus eventBus;

    private LevelData levelData;
    private LoadLevel levelLoader = new LoadLevel();

    private string[] levels; // All levels you may want to load
    private string activeLevel; // Current active level
    private int activeLevelIndex = 0; // Index of the current level in the levels array
    private string levelPath;


    public BlockContainer container = new BlockContainer();
    public Player player;
    public EntityContainer<Ball> ballsContainer = new EntityContainer<Ball>(100);
    public Ball ball;
    public TokenContainer tokenContainer = new TokenContainer();
    public Text livesAndTimeText;
    public Vec2F livesTextPosition1 = new Vec2F(0.1f, 0.5f);
    public Vec2F livesTextPosition2 = new Vec2F(0.9f, 0.5f);
    public long initialTime;
    public long timeElapsed;
    public bool levelTimeExists;
    public string? timeOfLevel;
    public static GameRunning GetInstance()
    {
        if (instance == null)
        {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.ResetState();
        }
        return GameRunning.instance;
    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
    {
        switch (action)
        {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
            default:
                break;
        }
    }

    private void KeyPress(KeyboardKey key)
    {
        switch (key)
        {
            case KeyboardKey.Left:
                GameEvent moveLeft = new GameEvent
                {
                    EventType = GameEventType.MovementEvent,
                    Message = "MOVE_LEFT"
                };
                eventBus.RegisterEvent(moveLeft);
                break;

            case KeyboardKey.Right:
                GameEvent moveRight = new GameEvent
                {
                    EventType = GameEventType.MovementEvent,
                    Message = "MOVE_RIGHT"
                };
                eventBus.RegisterEvent(moveRight);
                break;
            default:
                break;
        }
    }

    private void KeyRelease(KeyboardKey key)
    {
        switch (key)
        {
            case KeyboardKey.Left:
                GameEvent stopMoveLeft = new GameEvent
                {
                    EventType = GameEventType.MovementEvent,
                    Message = "STOP_MOVE_LEFT"
                };
                eventBus.RegisterEvent(stopMoveLeft);
                break;

            case KeyboardKey.Right:
                GameEvent stopMoveRight = new GameEvent
                {
                    EventType = GameEventType.MovementEvent,
                    Message = "STOP_MOVE_RIGHT"
                };
                eventBus.RegisterEvent(stopMoveRight);
                break;

            case KeyboardKey.Escape:
                BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent
                        {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_PAUSED"
                        });
                break;

            case KeyboardKey.Space:
                ball.movementSwitch = true;
                activeLevelIndex++;
                // Below is for looping back to level 1 instead of returning to main menu:
                // activeLevelIndex %= levels.Length;

                // Reset level index and return to main menu if no more levels
                if (activeLevelIndex >= levels.Length)
                {
                    activeLevelIndex = 0;

                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent
                        {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "MAIN_MENU"
                        });
                    break;
                }
                ResetState(); // Reset state to load the new level
                break;

            case KeyboardKey.Enter:
                ball.movementSwitch = true;

                break;

            default:
                break;
        }
    }

    public void ResetLevel()
    {
        activeLevelIndex = 0;
    }

    public void RenderState()
    {
        container.blocks.Iterate(block => { });
        ballsContainer.Iterate(ball => { });
        container.blocks.RenderEntities();
        tokenContainer.tokens.RenderEntities();
        player.Render();
        foreach (Ball ball in ballsContainer) {
        ball.Render();
        }

        livesAndTimeText.RenderText();
    }


    public void ResetState()
    {
        eventBus = BreakoutBus.GetBus();

        levelPath = Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Levels");
        levels = Directory.GetFiles(levelPath);

        Console.WriteLine($"Loading file: {levels[activeLevelIndex]} ...");

        activeLevel = levels[activeLevelIndex];

        // levelName = "level1"; // should be changed so you can change levels dynamically.
        // levelPath = Path.Combine("Assets", "Levels", activeLevel + ".txt");
        levelData = levelLoader.ReadLevelFile(activeLevel);

        if (levelData == null)
        {
            Console.Error.WriteLine($"Error reading level file, please read above error message. Aborting run...");
            GameEvent exitGame = new GameEvent
            {
                EventType = GameEventType.WindowEvent,
                Message = "CLOSE_WINDOW"
            };
            eventBus.RegisterEvent(exitGame);
            return;
        }
        container.blocks.Iterate(block =>
        {
            block.DeleteEntity();
        });
        
        container.CreateBlocks(levelData);

        player = new Player(
            new DynamicShape(new Vec2F(0.4f, 0.025f), new Vec2F(0.2f, 0.025f)),
            new Image(Path.Combine(workingDirectory, "..", "Breakout", "Assets", "Images", "player.png")));
        ballsContainer.Iterate(ball =>
        {
            ball.DeleteEntity();
        });
        ball = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));
        ballsContainer.AddEntity(ball);
        eventBus.Subscribe(GameEventType.MovementEvent, player);
        eventBus.Subscribe(GameEventType.StatusEvent, tokenContainer);

        livesAndTimeText = new Text("Lives:", livesTextPosition1, livesTextPosition2);
        livesAndTimeText.SetColor(new Vec4F(1.0f, 1.0f, 1.0f, 1.0f));
        livesAndTimeText.RenderText();

        
        levelTimeExists = levelData.MetaDictionary.ContainsKey("Time");
        
        if (levelTimeExists) {
            timeOfLevel = levelData.MetaDictionary["Time"];
        }
        
        initialTime = DIKUArcade.Timers.StaticTimer.GetElapsedMilliseconds(); 
    }

    public void UpdateState()
    {
        eventBus.ProcessEventsSequentially();
        player.Move();

        foreach (Ball ball in ballsContainer) {
        ball.Move();
        ball.UpdateDirection();
        }
        ballsContainer.Iterate(ball => {});
        
        if (ballsContainer.CountEntities() == 0 && player.Lives != 0) { 
            player.Lives -=1 ; 
            ball = new Ball(
            new DynamicShape(new Vec2F(0.5f, 0.05f), new Vec2F(0.05f, 0.05f)),
            new Image(Path.Combine("Assets", "Images", "ball.png")));
            ballsContainer.AddEntity(ball);
        }
    
        if (player.Lives == 0) {
            BreakoutBus.GetBus().RegisterEvent(
            new GameEvent
            {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_OVER"
            });
        }
        if (levelTimeExists){
            if ( long.Parse(timeOfLevel) - timeElapsed < 1 ) {
                BreakoutBus.GetBus().RegisterEvent(
                new GameEvent
                {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_OVER"
                });
            }
        }

        if (container.blocks.CountEntities() <= 0) {
                if (activeLevelIndex >= levels.Length-1){
                    BreakoutBus.GetBus().RegisterEvent(
                        new GameEvent
                        {
                            EventType = GameEventType.GameStateEvent,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_WON"
                        });
                } else {
                    activeLevelIndex++;
                    ResetState();
                    }

        }

        if (levelTimeExists) {
            livesAndTimeText.SetText($"Lives:{player.Lives}Time:{long.Parse(timeOfLevel)- timeElapsed}");
        } else {
            livesAndTimeText.SetText($"Lives: {player.Lives}");
            
        }
        timeElapsed = (DIKUArcade.Timers.StaticTimer.GetElapsedMilliseconds() - initialTime) / 1000; 
        

        tokenContainer.tokens.Iterate(token =>
        {
            token.Move();
            if (token.Position.Y < 0.0f)
            {
                token.DeleteEntity();
            }
        });
        
        CollisionHandler.HandleCollisions(container.blocks, ballsContainer, player, tokenContainer.tokens);
    }
}