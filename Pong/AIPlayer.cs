using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;
using System.Threading;

namespace Pong
{
    /// <summary>
    /// This class represents an AI controlled pad.
    /// </summary>
    public sealed class AIPlayer : AgentBase
    {
        private Random rng;
        private double initialX;
        private double initialY;
        private readonly int height;
        private readonly int width;

        public AIPlayer(int playerId, string playerName, ISpace ts) : base(playerName, ts)
        {
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
            this.rng = new Random(Environment.TickCount);
            this.PlayerId = playerId;
            this.Name = playerName;
            this.Put(EntityType.PLAYERINFO, playerId, playerName, 0);
            this.initialX = this.PlayerId == 1 ? 0d : (double)(this.width - 1d);
            this.initialY = (double)(this.height / 2d);
            this.Put(EntityType.POSITION, this.PlayerId, this.initialX, this.initialY);
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query(EntityType.SIGNAL, "start");

            // Keep iterating while the state is 'running'
            while (this.QueryP(EntityType.SIGNAL, "running", true) != null)
            {
                // Get the player position
                Position playerPosition = (Position)this.Get(EntityType.POSITION, this.PlayerId, typeof(double), typeof(double));

                // Check if we have to serve, if so reset the player position and serve
                if (this.GetP(EntityType.SIGNAL, "serving", this.Name) != null)
                {
                    playerPosition.Y = this.initialY;
                    this.Serve(playerPosition.X, playerPosition.Y);
                }

                // Try to read the latest position, direction and speed of the pong.
                Pong pong = (Pong)this.QueryP(EntityType.PONG, typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
                if (pong != null)
                {
                    // We know the pong information, so let the AI move towards the pong and attempt to catch it.
                    double playerY = playerPosition.Y;
                    double pongY = pong.Position.Y;
                    playerY += (playerY < pongY) ? 0.5d : -0.5d;
                    playerY = Math.Max(playerY, 0d);
                    playerY = Math.Min(playerY, (double)(this.height - 1));
                    playerPosition.Y = playerY;
                }
                this.Put(playerPosition);
                Thread.Sleep(40);
            }
        }

        private void Serve(double x, double y)
        {
            this.Put(EntityType.PONG, x, y, this.GetServeDirection(), (this.rng.NextDouble() * 0.51d) - 0.25d, 2.5d);
        }

        private double GetServeDirection()
        {
            return PlayerId == 1 ? 1d : -1d;
        }
    }
}
