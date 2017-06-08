using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lifeforms
{
    public class Lifeform : AgentBase
    {
        protected int width;
        protected int height;
        protected Random rng;

        public Lifeform(long genom, long p1genom, long p2genom, int life, int food, int x, int y, int generation, int width, int height, ISpace ts) : base(genom.ToString(), ts)
        {
            this.rng = new Random(Environment.TickCount);
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
        }

        public long Genom { get; set; }
        public long P1Genom { get; set; }
        public long P2Genom { get; set; }
        public int InitialLife { get; set; }
        public int Life { get; set; }
        public int Food { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Generation { get; set; }
        public int BreedingCost { get { return 50; } }

        protected override void DoWork()
        {
            // Wait until we can start
            this.ts.Query("start");
            this.ts.Put("lifeform", this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, this.X, this.Y, this.Generation);
            ITuple targetMate = null;
            ITuple targetFood = null;
            int roamX = 0;
            int roamY = 0;
            // Keep iterating while the state is 'running'
            while (this.ts.QueryP("running", true) != null && this.Life > 0)
            {
                // if the lifeform has more food than the cost to breed, then find a mate
                if (this.Food > this.BreedingCost)
                {
                    // is the mate close by?
                    if (targetMate != null && this.IsAdjacent((int)targetMate[5], (int)targetMate[6]))
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
                            targetMate = this.ts.QueryP("lifeform", (long)targetMate[1], (long)targetMate[2], (long)targetMate[3], (int)targetMate[4], typeof(int), typeof(int), typeof(int));
                            if (targetMate != null)
                            {
                                this.Move((int)targetMate[5], (int)targetMate[6]);
                            }
                        }
                        // otherwise roam
                        else
                        {
                            if (this.X == roamX && this.Y == roamY)
                            {
                                roamX = (this.rng.Next() % (this.width - 2)) + 1;
                                roamY = (this.rng.Next() % (this.height - 2)) + 1;
                            }
                            this.Move(roamX, roamY);
                        }
                    }
                }
                // otherwise search for food
                else
                {
                    if (targetFood != null && this.IsAdjacent((int)targetFood[3], (int)targetFood[4]))
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
                    }
                }
                this.Food--;
                if (this.Food < 0)
                {
                    this.Life -= 5;
                }
                Thread.Sleep(50);
            }
            this.GetCurrentLifeform(typeof(int), typeof(int));
        }
        private ITuple GetCurrentLifeform(object x, object y)
        {
            return this.ts.Get("lifeform", this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, x, y, this.Generation);
        }
        private bool IsAdjacent(int x, int y)
        {
            return Math.Abs(this.X - x) <= 1 && Math.Abs(this.Y - y) <= 1;
        }
        private void Eat(ITuple food)
        {
            if (this.ts.GetP("food", food[1], food[2], food[3], food[4]) != null)
            {
                int amount = (int)food[2];
                this.Food += amount;
                this.Life = this.InitialLife;
                this.ts.Put("foodEaten");
            }
        }
        private ITuple FindMate()
        {

            IEnumerable<ITuple> targetmates = this.ts.QueryAll("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int));
            targetmates = targetmates.Where(lf => this.CanBreed(lf) && this.CanSee(lf));
            if (targetmates.Count() > 0)
            {
                return targetmates.ElementAt(rng.Next() % targetmates.Count());
            }
            return null;
        }
        private ITuple FindFood()
        {
            IEnumerable<ITuple> targetFoods = this.ts.QueryAll("food", typeof(int), typeof(int), typeof(int), typeof(int));
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
            ITuple me = this.GetCurrentLifeform(this.X, this.Y);

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
            this.ts.Put("lifeform", this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, this.X, this.Y, this.Generation);
        }
        private bool CanSee(ITuple other)
        {
            int x = (int)other[5];
            int y = (int)other[6];
            return Math.Abs(this.X - x) <= 8 && Math.Abs(this.Y - y) <= 8;
        }
        private void GetFreeAdjacentCell(out int x, out int y)
        {
            ITuple adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X, this.Y + 1, typeof(int));
            if (adjacent == null)
            {
                adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X + 1, this.Y, typeof(int));
                if (adjacent == null)
                {
                    adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X - 1, this.Y, typeof(int));
                    if (adjacent == null)
                    {
                        adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X, this.Y - 1, typeof(int));
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

            long otherGenom = (long)other[1];
            long otherP1genom = (long)other[2];
            long otherP2genom = (long)other[3];

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
                this.Food -= this.BreedingCost;
                long otherGenom = (long)other[1];
                int otherInitialLife = (int)other[4];
                int otherGeneration = (int)other[7];
                int initiallife = (int)(((this.InitialLife + otherInitialLife) / 2) * (1.0 + (this.rng.NextDouble() - 0.5)));
                int food = 0;
                x = x == this.width ? this.X - 1 : X;
                y = y == this.width ? this.Y - 1 : Y;
                long mutation = rng.Next() % (this.Genom + otherGenom);
                int maxGeneration = Math.Max(this.Generation, otherGeneration);
                this.ts.Put("spawn", (this.Genom * otherGenom) + mutation, this.Genom, otherGenom, initiallife, food, x, y, maxGeneration + 1);
            }
        }
    }
}
