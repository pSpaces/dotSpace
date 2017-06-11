using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Threading;
using System.Windows;

namespace Pong
{
    public class Pong : AgentBase
    {
        private Random rng;
        private int width;
        private int height;

        public Pong(ISpace ts, int width, int height) : base(string.Empty, ts)
        {
            this.width = width;
            this.height = height;
            this.rng = new Random(Environment.TickCount);
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query("start");
            int leftPlayerId = 1;
            int rightPlayerId = 2;
            
            // Read the player names
            ITuple leftplayer = this.Query(leftPlayerId, typeof(string));
            ITuple rightplayer = this.Query(rightPlayerId, typeof(string));

            // Keep iterating while the state is 'running'
            while (this.QueryP("running", true) != null)
            {
                // Get the position so we can update it
                // "pong", position:(x,y), normalized direction(x,y), speed: z
                ITuple pong = this.Get("pong", typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
                
                // Calculate new position based on the direction
                Vector position = new Vector((double)pong[1], (double)pong[2]);
                Vector direction = new Vector((double)pong[3], (double)pong[4]);
                Vector newPosition = position + direction * (double)pong[5];

                // Check if the pong is beyond top and bottom walls
                // if is has hit the walls, then change the direction
                if (newPosition.Y < 0 || newPosition.Y >= this.height)
                {
                    direction = this.ChangeDirection(direction, false);
                    newPosition = position + direction * (double)pong[5];
                }
                // Check if the poing has reached the left most wall
                if (newPosition.X <= 0)
                {
                    // Read the latest position of the player
                    ITuple playerPosition = this.Query(leftPlayerId, typeof(double), typeof(double));
                    // Check if the pong hit the player pad, then change the position and direction
                    // otherwise reward the other player
                    if ((int)(double)playerPosition[2] == (int)newPosition.Y )
                    {
                        direction = this.ChangeDirection(direction, true);
                        newPosition = position + direction * (double)pong[5];
                    }
                    else
                    {
                        this.IncreasePlayerScore((string)rightplayer[1], (string)leftplayer[1]);
                        continue;
                    }
                }
                // Check if the poing has reached the right most wall
                if (newPosition.X >= this.width)
                {
                    // Read the latest position of the player
                    ITuple playerPosition = this.Query(rightPlayerId, typeof(double), typeof(double));
                    // Check if the pong hit the player pad, then change the position and direction
                    // otherwise reward the other player
                    if ((int)(double)playerPosition[2] == (int)newPosition.Y)
                    {
                        direction = this.ChangeDirection(direction, true);
                        newPosition = position + direction * (double)pong[5];
                    }
                    else
                    {
                        this.IncreasePlayerScore((string)leftplayer[1], (string)rightplayer[1]);
                        continue;
                    }
                }

                // Update the pong information based on potential changes
                double pongY = Math.Max(newPosition.Y, 0d);
                pongY = Math.Min(pongY, (double)(this.height - 1));
                pong[1] = newPosition.X;
                pong[2] = pongY;
                pong[3] = direction.X;
                pong[4] = direction.Y;
                this.Put(pong);
                Thread.Sleep(100);
            }
        }

        private void IncreasePlayerScore(string playername, string serving)
        {
            ITuple playerScore = this.Get(playername, typeof(int));
            playerScore[1] = (int)playerScore[1] + 1;
            this.Put(playerScore);
            this.Put("serving", serving);
        }

        private Vector ChangeDirection(Vector currentDirection, bool xAxis)
        {
            Vector dir = new Vector();
            if (xAxis)
            {
                dir = new Vector(-currentDirection.X, -currentDirection.Y) + new Vector(rng.NextDouble() * 0.05, rng.NextDouble() * 0.05);
            }
            else
            {
                dir = new Vector(currentDirection.X, -currentDirection.Y) + new Vector(rng.NextDouble() * 0.05, rng.NextDouble() * 0.05);
            }
            dir.Normalize();
            return dir;
        }

    }
}
