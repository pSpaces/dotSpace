using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Threading;

namespace Lifeforms
{
    public class FoodDispenser : AgentBase
    {
        private Random rng;
        private int width;
        private int height;

        public FoodDispenser(ISpace ts, int width, int height) : base(string.Empty, ts)
        {
            this.width = width;
            this.height = height;
            this.rng = new Random(Environment.TickCount);
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query(EntityType.SIGNAL, "start");
            this.NewFood();
            this.NewFood();
            this.NewFood();

            // Keep iterating while the state is 'running'
            while (this.QueryP(EntityType.SIGNAL, "running", true) != null)
            {
                if (this.GetP(EntityType.SIGNAL, "foodEaten") != null)
                {
                    this.NewFood();
                }
                // time left, amount, position(x,y)
                Food food = (Food)this.GetP(EntityType.FOOD, typeof(int), typeof(int), typeof(int), typeof(int));
                if (food != null)
                {
                    food.TimeLeft--;
                    if (food.TimeLeft > 0)
                    {
                        this.Put(food);
                    }
                    else 
                    {
                        this.NewFood();
                    }
                }
                else
                {
                    this.NewFood();
                }
                Thread.Sleep(100);
            }
        }

        private void NewFood()
        {
            int rnd = this.rng.Next() % 100;
            if (rnd > 1)
            {
                int amount = (this.rng.Next() % 250)+50;
                int timeleft = this.rng.Next() % 500;
                int x = (this.rng.Next() % (this.width - 2)) + 1;
                int y = (this.rng.Next() % (this.height - 2)) + 1;
                this.Put(EntityType.FOOD, amount, timeleft, x, y);
                if (rnd > 98)
                {
                    this.NewFood();
                }
            }
        }

    }
}
