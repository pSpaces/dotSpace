using dotSpace.Interfaces;
using Pong.BaseClasses;
using System;
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
            this.ts.Query("start");
            while (this.ts.QueryP("running", true) != null)
            {
                ITuple playerPosition = this.ts.Get(this.PlayerId, typeof(double), typeof(double));
                if (this.ts.GetP("serving", this.Name) != null)
                {
                    playerPosition[2] = this.initialY;
                    this.Serve(new Vector((double)playerPosition[1], (double)playerPosition[2]));
                }
                ITuple pong = this.ts.QueryP("pong", typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
                if (pong != null)
                {
                    double playerY = (double)playerPosition[2];
                    double pongY = (double)pong[2];
                    playerY += (playerY < pongY) ? 0.25d : -0.25d;
                    playerY = Math.Max(playerY, 0d);
                    playerY = Math.Min(playerY, (double)(this.height - 1));
                    playerPosition[2] = playerY;
                }
                this.ts.Put(playerPosition);
            }
        }


    }
}
