using dotSpace.BaseClasses;
using dotSpace.Interfaces;
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
        private int height;
        private int width;

        public Game(int width, int height, ISpace ts)
        {
            this.rng = new Random(Environment.TickCount);
            this.ts = ts;
            this.width = width;
            this.height = height;
            this.food = new Food(ts, width, height);
            this.view = new View(width, height, ts);
            this.lifeformDispatcher = new LifeformDispatcher(this.width, this.height, this.ts);
        }

        public void AddLifeform(long genom, int life, int food)
        {
            int x = (this.rng.Next() % (this.width - 2)) + 1;
            int y = (this.rng.Next() % (this.height - 2)) + 1;
            this.ts.Put("spawn", genom, genom, genom, life, food, x, y, 0, 12, 4, 0);
        }

        public void Run()
        {
            this.ts.Put("running", true);
            this.view.Start();
            this.food.Start();
            this.lifeformDispatcher.Start();
            this.ts.Put("start");
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
