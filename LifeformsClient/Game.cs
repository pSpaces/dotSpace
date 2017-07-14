using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using Lifeforms;
using System;
using Test;

namespace LifeformsClient
{
    public class Game
    {
        private Random rng;
        private AgentBase lifeformDispatcher;
        private ISpace ts;
        private int height;
        private int width;

        public Game(ISpace ts)
        {
            this.rng = new Random(Environment.TickCount);
            this.ts = ts;
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
            this.lifeformDispatcher = new LifeformDispatcher(this.ts);
        }

        public void AddLifeform(long genom)
        {
            int x = (this.rng.Next() % (this.width - 2)) + 1;
            int y = (this.rng.Next() % (this.height - 2)) + 1;
            int life = (this.rng.Next() % 100) + 350;
            int food = (this.rng.Next() % 20) + 50;
            this.ts.Put(EntityType.SPAWN, genom, genom, genom, life, food, x, y, 0, 12, 4, 0);
        }

        public void Run()
        {
            this.ts.Put(EntityType.SIGNAL, "running", true);
            this.lifeformDispatcher.Start();
            this.ts.Put(EntityType.SIGNAL, "start");
        }

        public void Stop()
        {
            this.ts.Get(EntityType.SIGNAL, "running", true);
        }
    }
}
