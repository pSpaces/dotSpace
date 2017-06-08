using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Threading;

namespace Lifeforms
{
    public class Food : AgentBase
    {
        private Random rng;
        private int width;
        private int height;

        public Food(ISpace ts, int width, int height) : base(string.Empty, ts)
        {
            this.width = width;
            this.height = height;
            this.rng = new Random(Environment.TickCount);
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.ts.Query("start");
            this.NewFood();
            this.NewFood();
            this.NewFood();
            // Keep iterating while the state is 'running'
            while (this.ts.QueryP("running", true) != null)
            {
                if (this.ts.GetP("foodEaten") != null)
                {
                    this.NewFood();
                }
                // time left, amount, position(x,y)
                ITuple food = this.ts.GetP("food", typeof(int), typeof(int), typeof(int), typeof(int));
                if (food != null)
                {
                    int timeleft = (int)food[1] - 1;
                    if (timeleft > 0)
                    {
                        food[1] = timeleft;
                        this.ts.Put(food);
                    }
                    else if ((this.rng.Next() % 100) != 31)
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
            int amount = (int)(this.rng.NextDouble() * 100);
            int timeleft = (int)(this.rng.NextDouble() * 500);
            int x = (this.rng.Next() % (this.width - 2)) + 1;
            int y = (this.rng.Next() % (this.height - 2)) + 1;
            this.ts.Put("food", amount, timeleft, x, y);
            if ((this.rng.Next() % 100) == 31)
            {
                this.NewFood();
            }
        }

    }
}
