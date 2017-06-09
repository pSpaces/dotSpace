using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using Lifeforms;
using System;

namespace LifeformsServer
{
    public class Game
    {
        private Random rng;
        private AgentBase food;
        private View view;

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
            this.food = new Food(ts, width, height);
        }

        public void Run()
        {
            this.ts.Put("running", true);
            this.food.Start();
            this.view.Start();
            this.ts.Put("start");
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
