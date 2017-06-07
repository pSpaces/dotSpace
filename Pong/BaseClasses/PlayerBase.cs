using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Windows;

namespace Pong.BaseClasses
{
    public abstract class PlayerBase : AgentBase
    {
        protected Random rng;
        protected int width;
        protected int height;
        protected double initialX;
        protected double initialY;

        public PlayerBase(int playerId, string playerName, int width, int height, ISpace ts) : base(playerName, ts)
        {
            this.width = width;
            this.height = height;
            this.rng = new Random(Environment.TickCount);
            this.PlayerId = playerId;
            this.Name = playerName;
            this.ts.Put(playerId, playerName);
            this.ts.Put(playerName, 0);
            this.initialX = this.PlayerId == 1 ? 0d : (double)(width - 1d);
            this.initialY = (double)(height / 2d);
            this.ts.Put(this.PlayerId, this.initialX, this.initialY);
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }

        protected void Serve(Vector position)
        {
            this.ts.Put("pong", position.X, position.Y, this.GetServeDirection(), (this.rng.NextDouble()*0.51d)-0.25d, 2.5d);
        }

        private double GetServeDirection()
        {
            return PlayerId == 1 ? 1d : -1d;
        }
    }
}
