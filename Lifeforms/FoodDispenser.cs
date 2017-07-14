using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;
using System.Threading;

namespace Lifeforms
{
    /// <summary>
    /// This class is responsible for creating food for the lifeforms.
    /// </summary>
    public class FoodDispenser : AgentBase
    {
        private Random rng;
        private int width;
        private int height;

        public FoodDispenser(ISpace ts) : base(string.Empty, ts)
        {
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
            this.rng = new Random(Environment.TickCount);
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query(EntityType.SIGNAL, "start");
            
            // Create 3 initial foods
            this.NewFood();
            this.NewFood();
            this.NewFood();

            // Keep iterating while the state is 'running'
            while (this.QueryP(EntityType.SIGNAL, "running", true) != null)
            {
                // If food was eaten then try to spawn a new
                if (this.GetP(EntityType.SIGNAL, "foodEaten") != null)
                {
                    this.NewFood();
                }
                // Get a food and process it
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

        /// <summary>
        /// Creates new food. This method is however not deterministic in that food may or may not appear. 
        /// This is deliberately done to createa a more dynamic environment.
        /// </summary>
        private void NewFood()
        {
            // There's a 1% chance that a given food will not spawn
            int rnd = this.rng.Next() % 100;
            if (rnd > 1)
            {
                int amount = (this.rng.Next() % 250)+50;
                int timeleft = this.rng.Next() % 500;
                int x = (this.rng.Next() % (this.width - 2)) + 1;
                int y = (this.rng.Next() % (this.height - 2)) + 1;
                this.Put(EntityType.FOOD, amount, timeleft, x, y);
                // Theres a 1% chance that an additional food will spawn.
                if (rnd > 98)
                {
                    this.NewFood();
                }
            }
        }

    }
}
