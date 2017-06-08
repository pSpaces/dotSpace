using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using Lifeforms;
using System;
using Test;

namespace LifeformsClient
{
    public class Game
    {
        private Random rng;
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
            this.view = new View(width, height, ts);
            this.lifeformDispatcher = new LifeformDispatcher(this.width, this.height, this.ts);
        }

        public void AddLifeform(long genom)
        {
            int x = (this.rng.Next() % (this.width - 2)) + 1;
            int y = (this.rng.Next() % (this.height - 2)) + 1;
            int life = (this.rng.Next() % 100) + 350;
            int food = (this.rng.Next() % 20) + 50;
            this.ts.Put("spawn", genom, genom, genom, life, food, x, y, 0);
        }

        public void Run()
        {
            this.ts.Put("running", true);
            this.view.Start();
            this.lifeformDispatcher.Start();
            this.ts.Put("start");
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
