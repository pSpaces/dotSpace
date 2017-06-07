using dotSpace.Interfaces;
using Pong.BaseClasses;
using System;
using System.Threading;
using System.Windows;

namespace Pong
{
    public class AIPlayer : PlayerBase
    {
        public AIPlayer(int playerId, string playername, int width, int height, ISpace ts) : base(playerId, playername, width, height, ts)
        {
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.ts.Query("start");
            // Keep iterating while the state is 'running'
            while (this.ts.QueryP("running", true) != null)
            {
                // Get the player position
                ITuple playerPosition = this.ts.Get(this.PlayerId, typeof(double), typeof(double));
                // Check if we have to serve, if so reset the player position and serve
                if (this.ts.GetP("serving", this.Name) != null)
                {
                    playerPosition[2] = this.initialY;
                    this.Serve(new Vector((double)playerPosition[1], (double)playerPosition[2]));
                }
                // Try to read the latest position, direction and speed of the pong.
                // position: (x,y), direction(x,y), speed: f
                ITuple pong = this.ts.QueryP("pong", typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
                if (pong != null)
                {
                    // We know the pong information, so let the AI move towards the pong and attempt to catch it.
                    double playerY = (double)playerPosition[2];
                    double pongY = (double)pong[2];
                    playerY += (playerY < pongY) ? 0.5d : -0.5d;
                    playerY = Math.Max(playerY, 0d);
                    playerY = Math.Min(playerY, (double)(this.height - 1));
                    playerPosition[2] = playerY;
                }
                this.ts.Put(playerPosition);
                Thread.Sleep(40);
            }
        }
    }
}
