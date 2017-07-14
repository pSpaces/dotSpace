using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using Lifeforms;
using System;
using Test;

namespace LocalLifeforms
{
    public class Game
    {
        private Random rng;
        private AgentBase food;
        private View view;
        private AgentBase lifeformDispatcher;
        private ISpace ts;
        private readonly int width;
        private readonly int height;

        public Game(ISpace ts)
        {
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
            this.rng = new Random(Environment.TickCount);
            this.ts = ts;
            this.food = new FoodDispenser(ts);
            this.view = new View(ts);
            this.lifeformDispatcher = new LifeformDispatcher(this.ts);
        }

        public void AddLifeform(long genom, int life, int food)
        {
            int x = (this.rng.Next() % (this.width - 2)) + 1;
            int y = (this.rng.Next() % (this.height - 2)) + 1;
            this.ts.Put(EntityType.SPAWN, genom, genom, genom, life, food, x, y, 0, 12, 4, 0);
        }

        public void Run()
        {
            this.ts.Put(EntityType.SIGNAL, "running", true);
            this.view.Start();
            this.food.Start();
            this.lifeformDispatcher.Start();
            this.ts.Put(EntityType.SIGNAL, "start");
        }

        public void Stop()
        {
            this.ts.Get(EntityType.SIGNAL, "running", true);
        }
    }
}
