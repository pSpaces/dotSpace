using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lifeforms
{
    public sealed class Lifeform : AgentBase
    {
        private int width;
        private int height;
        private Random rng;

        private string id;
        private int roamX;
        private int roamY;
        private int nrChildren;
        private int maxSpeedgain;
        private int breedingCost;
        private int maxFood;
        private ITuple targetMate;
        private ITuple targetFood;

        public Lifeform(long genom, long p1genom, long p2genom, int life, int food, int x, int y, int generation, int visualRange, int maxNrChildren, int speed, int width, int height, ISpace ts) : base(genom.ToString(), ts)
        {
            this.id = Guid.NewGuid().ToString();
            this.rng = new Random(Environment.TickCount);
            this.nrChildren = 0;
            this.maxSpeedgain = 40;
            this.breedingCost = 50;
            this.maxFood = this.breedingCost * 2;
            this.targetMate = null;
            this.targetFood = null;
            this.Genom = genom;
            this.P1Genom = p1genom;
            this.P2Genom = p2genom;
            this.InitialLife = life;
            this.Life = life;
            this.Food = food;
            this.X = x;
            this.Y = y;
            this.width = width;
            this.height = height;
            this.Generation = generation;
            this.VisualRange = visualRange;
            this.MaxNrChildren = maxNrChildren;
            this.Speed = Math.Min(this.maxSpeedgain, speed);
        }

        private long Genom { get; set; }
        private long P1Genom { get; set; }
        private long P2Genom { get; set; }
        private int InitialLife { get; set; }
        private int Life { get; set; }
        private int Food { get; set; }
        private int X { get; set; }
        private int Y { get; set; }
        private int Generation { get; set; }
        private int VisualRange { get; set; }
        private int MaxNrChildren { get; set; }
        private int Speed { get; set; }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query("start");
            this.Put("lifeform", this.id, this.X, this.Y);
            this.Put("lifeformProperties", this.id, this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, this.Generation, this.VisualRange, this.MaxNrChildren, this.Speed);
            this.roamX = (this.rng.Next() % (this.width - 3)) + 1;
            this.roamY = (this.rng.Next() % (this.height - 3)) + 1;
            // Keep iterating while the state is 'running'
            while (this.QueryP("running", true) != null && this.Life > 0)
            {
                // if the lifeform has more food than the cost to breed, then find a mate
                if (this.Food > this.breedingCost && nrChildren < this.MaxNrChildren)
                {
                    // is the mate close by?
                    if (targetMate != null && this.IsNearby((int)targetMate[2], (int)targetMate[3]))
                    {
                        this.Breed(targetMate);
                        targetMate = null;
                    }
                    else
                    {
                        // We have no mate, so try to find one.
                        if (targetMate == null)
                        {
                            targetMate = this.FindMate();
                        }

                        // if we found one, then move towards it
                        if (targetMate != null)
                        {
                            // update the mate's location
                            if ((targetMate = this.QueryP("lifeform", (string)targetMate[1], typeof(int), typeof(int))) != null)
                            {
                                this.Move((int)targetMate[2], (int)targetMate[3]);
                            }
                        }
                        // otherwise roam
                        else
                        {
                            this.Roam();
                        }
                    }
                }
                // otherwise search for food
                else
                {
                    if (targetFood != null && this.IsNearby((int)targetFood[3], (int)targetFood[4]))
                    {
                        this.Eat(targetFood);
                        targetFood = null;
                    }
                    else
                    {
                        if (targetFood == null)
                        {
                            targetFood = this.FindFood();
                        }
                        if (targetFood != null)
                        {
                            this.Move((int)targetFood[3], (int)targetFood[4]);
                        }
                        // otherwise roam
                        else
                        {
                            this.Roam();
                        }
                    }
                }

                this.Food = Math.Max(this.Food - 1, 0);
                if (this.Food == 0)
                {
                    this.Life--;
                }
                Thread.Sleep(50 - this.Speed);
            }
            this.Get("lifeform", this.id, typeof(int), typeof(int));
            this.Get("lifeformProperties", this.id, typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
        }
        private void Roam()
        {
            if (this.X == this.roamX && this.Y == this.roamY)
            {
                this.roamX = (this.rng.Next() % (this.width - 3)) + 1;
                this.roamY = (this.rng.Next() % (this.height - 3)) + 1;
            }
            this.Move(this.roamX, this.roamY);
        }
        private bool IsNearby(int x, int y)
        {
            return Math.Abs(this.X - x) <= 1 && Math.Abs(this.Y - y) <= 1;
        }
        private void Eat(ITuple food)
        {
            if ((food = this.GetP("food", food[1], typeof(int), food[3], food[4])) != null)
            {
                int amount = (int)food[1];
                int foodDiff = this.maxFood - this.Food;
                if (foodDiff > 0)
                {
                    int eat = Math.Min(foodDiff, amount);
                    this.Food += eat;
                    amount -= eat;

                    if (amount > 0)
                    {
                        this.Put("food", amount, (int)food[2], (int)food[3], (int)food[4]);
                    }
                    else
                    {
                        this.Put("foodEaten");
                    }
                }
            }
        }
        private ITuple FindMate()
        {
            IEnumerable<ITuple> targetmates = this.QueryAll("lifeform", typeof(string), typeof(int), typeof(int));
            targetmates = targetmates.Where(lf => this.CanSee((int)lf[2], (int)lf[3]) && this.CanBreed(lf)).ToList();
            if (targetmates.Count() > 0)
            {
                return targetmates.ElementAt(rng.Next() % targetmates.Count());
            }
            return null;
        }
        private ITuple FindFood()
        {
            IEnumerable<ITuple> targetFoods = this.QueryAll("food", typeof(int), typeof(int), typeof(int), typeof(int));
            targetFoods = targetFoods.Where(f => this.CanSee((int)f[3], (int)f[4]));
            if (targetFoods.Count() > 0)
            {
                return targetFoods.ElementAt(rng.Next() % targetFoods.Count());
            }
            return null;
        }
        private void Move(int x, int y)
        {
            int deltaX = Math.Abs(this.X - x);
            int deltaY = Math.Abs(this.Y - y);
            this.Get("lifeform", this.id, typeof(int), typeof(int));

            if (deltaX > deltaY)
            {
                if (this.X > x)
                {
                    this.X--;
                }
                else if (this.X < x)
                {
                    this.X++;
                }
            }
            else
            {
                if (this.Y < y)
                {
                    this.Y++;
                }
                else if (this.Y > y)
                {
                    this.Y--;
                }
            }
            this.X = Math.Min(this.X, this.width - 2);
            this.X = Math.Max(this.X, 1);
            this.Y = Math.Min(this.Y, this.height - 2);
            this.Y = Math.Max(this.Y, 1);

            this.Put("lifeform", this.id, this.X, this.Y);
        }
        private bool CanSee(int x, int y)
        {
            int deltaX = Math.Abs(this.X - x);
            int deltaY = Math.Abs(this.Y - y);
            int dist = (int)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return dist <= this.VisualRange;
        }
        private void GetFreeAdjacentCell(out int x, out int y)
        {
            ITuple adjacent = this.QueryP("lifeform", typeof(string), this.X, this.Y + 1);
            if (adjacent == null)
            {
                adjacent = this.QueryP("lifeform", typeof(string), this.X + 1, this.Y);
                if (adjacent == null)
                {
                    adjacent = this.QueryP("lifeform", typeof(string), this.X - 1, this.Y);
                    if (adjacent == null)
                    {
                        adjacent = this.QueryP("lifeform", typeof(string), this.X, this.Y - 1);
                        if (adjacent == null)
                        {
                            x = -1;
                            y = -1;
                        }
                        x = this.X;
                        y = this.Y - 1;
                    }
                    x = this.X;
                    y = this.Y - 1;
                }
                x = this.X + 1;
                y = this.Y;
            }
            x = this.X;
            y = this.Y + 1;
        }
        private bool CanBreed(ITuple other)
        {
            if (other == null)
            {
                return false;
            }
            ITuple lifeformProperties = this.QueryP("lifeformProperties", other[1], typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            if (lifeformProperties == null)
            {
                return false;
            }
            long otherGenom = (long)lifeformProperties[2];
            long otherP1genom = (long)lifeformProperties[3];
            long otherP2genom = (long)lifeformProperties[4];

            return (this.Genom != otherP1genom && this.Genom != otherP2genom) &&
                   (this.P1Genom != otherP1genom && this.P1Genom != otherP2genom) &&
                   (this.P2Genom != otherP1genom && this.P2Genom != otherP2genom);
        }
        private void Breed(ITuple other)
        {
            int x = -1;
            int y = -1;
            this.GetFreeAdjacentCell(out x, out y);
            if (x > 0 && y > 0)
            {
                ITuple mateProperties = this.QueryP("lifeformProperties", other[1], typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
                if (mateProperties != null)
                {
                    x = x == this.width ? this.X - 1 : X;
                    y = y == this.width ? this.Y - 1 : Y;

                    this.Life -= this.InitialLife / this.MaxNrChildren;
                    this.Food -= this.breedingCost;
                    long otherGenom = (long)mateProperties[2];
                    int otherInitialLife = (int)mateProperties[5];
                    int otherGeneration = (int)mateProperties[6];
                    int otherVisualRange = (int)mateProperties[7];
                    int othermaxNrChildren = (int)mateProperties[8];
                    int otherSpeed = (int)mateProperties[9];

                    long genom = 31 + this.Genom;
                    genom = (genom * 31) + otherGenom;

                    int initiallife = (this.InitialLife + otherInitialLife) / 2;
                    initiallife += (this.rng.Next() % (initiallife / 2)) - (initiallife / 4);

                    int food = 0;
                    int generation = Math.Max(this.Generation, otherGeneration) + 1;

                    int visualRange = (this.VisualRange + otherVisualRange) / 2;
                    visualRange += (this.rng.Next() % visualRange) - ((visualRange - 1) / 2);

                    int maxNrChildren = ((this.MaxNrChildren + othermaxNrChildren) / 2) + (this.rng.Next() % 5) - 2;
                    int speed = Math.Max(this.Speed, otherSpeed) + (this.rng.Next() % 5) - 2;

                    this.Put("spawn", genom, this.Genom, otherGenom, initiallife, food, x, y, generation, visualRange, maxNrChildren, speed);
                    this.nrChildren++;
                }
            }
        }
    }
}
