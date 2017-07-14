using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
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

        public Game(ISpace ts)
        {
            this.rng = new Random(Environment.TickCount);
            this.ts = ts;
            this.view = new View(ts);
            this.food = new FoodDispenser(ts);
        }

        public void Run()
        {
            this.ts.Put(EntityType.SIGNAL, "running", true);
            this.food.Start();
            this.view.Start();
            this.ts.Put(EntityType.SIGNAL,"start");
        }

        public void Stop()
        {
            this.ts.Get(EntityType.SIGNAL, "running", true);
        }
    }
}
