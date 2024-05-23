using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using DIKUArcade.Entities;
namespace Breakout;

public static class CollisionHandler
{
    public static void HandleCollisions(EntityContainer<Block> blocksContainer, EntityContainer<Ball> ballsContainer, Player player, EntityContainer<Token> tokenContainer)
    {
        foreach (Block block in blocksContainer)
        {
            foreach (Ball ball in ballsContainer)
            {
                if (CollisionDetection.Aabb(ball.shape, block.shape).Collision)
                {

                    if (!player.Fire || block.health != 1)
                    {
                        if (CollisionDetection.Aabb(ball.shape, block.shape).CollisionDir == CollisionDirection.CollisionDirUp ||
                                                CollisionDetection.Aabb(ball.shape, block.shape).CollisionDir == CollisionDirection.CollisionDirDown ||
                                                CollisionDetection.Aabb(ball.shape, block.shape).CollisionDir == CollisionDirection.CollisionDirDown)
                        {
                            ball.ReverseYDirection();
                        }
                        else
                        {
                            ball.ReverseXDirection();
                        }
                    }
                    block.Hit();

                }

                if (CollisionDetection.Aabb(ball.shape, player.shape()).Collision)
                {
                    if (CollisionDetection.Aabb(ball.shape, player.shape()).CollisionDir == CollisionDirection.CollisionDirRight ||
                        CollisionDetection.Aabb(ball.shape, player.shape()).CollisionDir == CollisionDirection.CollisionDirLeft)
                    {
                        ball.ReverseXDirection();
                        ball.ReverseYDirection();
                    }
                    else
                    {
                        ball.BounceFromPlayer(player);
                    }
                }
            }
        }
        foreach (Token token in tokenContainer)
        {
            if (CollisionDetection.Aabb(token.shape, player.shape()).Collision)
            {
                player.PowerUp(token.name);
                token.DeleteEntity();
            }

        }
    }
}

