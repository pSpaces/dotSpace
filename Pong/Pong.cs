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
            this.ts.Query("start");
            int leftPlayerId = 1;
            int rightPlayerId = 2;
            ITuple leftplayer = this.ts.Query(leftPlayerId, typeof(string));
            ITuple rightplayer = this.ts.Query(rightPlayerId, typeof(string));
            while (this.ts.QueryP("running", true) != null)
            {
                // "pong", position:(x,y), normalized direction(x,y), speed: z
                ITuple pong = this.ts.Get("pong", typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
                Vector position = new Vector((double)pong[1], (double)pong[2]);
                Vector direction = new Vector((double)pong[3], (double)pong[4]);
                Vector newPosition = position + direction * (double)pong[5];

                if (newPosition.Y < 0 || newPosition.Y >= this.height)
                {
                    direction = this.ChangeDirection(direction, false);
                    newPosition = position + direction * (double)pong[5];
                }
                if (newPosition.X <= 0)
                {
                    ITuple playerPosition = this.ts.Query(leftPlayerId, typeof(double), typeof(double));
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
                if (newPosition.X >= this.width)
                {
                    ITuple playerPosition = this.ts.Query(rightPlayerId, typeof(double), typeof(double));
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

                double pongY = Math.Max(newPosition.Y, 0d);
                pongY = Math.Min(pongY, (double)(this.height - 1));
                pong[1] = newPosition.X;
                pong[2] = pongY;
                pong[3] = direction.X;
                pong[4] = direction.Y;
                this.ts.Put(pong);
                Thread.Sleep(100);
            }
        }

        private void IncreasePlayerScore(string playername, string serving)
        {
            ITuple playerScore = this.ts.Get(playername, typeof(int));
            playerScore[1] = (int)playerScore[1] + 1;
            this.ts.Put(playerScore);
            this.ts.Put("serving", serving);
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
