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

        private int nrChildren;

        public Lifeform(long genom, long p1genom, long p2genom, int life, int food, int x, int y, int generation, int visualRange, int maxNrChildren, int speed, int width, int height, ISpace ts) : base(genom.ToString(), ts)
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
            this.VisualRange = visualRange;
            this.nrChildren = 0;
            this.MaxNrChildren = maxNrChildren;
            this.Speed = speed;
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
        private int BreedingCost { get { return 50; } }
        private int MaxNrChildren { get; set; }
        private int Speed { get; set; }
        private int MaxSpeedgain { get { return 25; } }
        private int RoamX { get; set; }
        private int RoamY { get; set; }

        protected override void DoWork()
        {
            // Wait until we can start
            this.ts.Query("start");
            this.ts.Put("lifeform", this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, this.X, this.Y, this.Generation, this.VisualRange, this.MaxNrChildren, this.Speed);
            ITuple targetMate = null;
            ITuple targetFood = null;
            this.RoamX = (this.rng.Next() % (this.width - 3)) + 1;
            this.RoamY = (this.rng.Next() % (this.height - 3)) + 1;
            // Keep iterating while the state is 'running'
            while (this.ts.QueryP("running", true) != null && this.Life > 0)
            {
                // if the lifeform has more food than the cost to breed, then find a mate
                if (this.Food > this.BreedingCost && nrChildren < this.MaxNrChildren)
                {
                    // is the mate close by?
                    if (targetMate != null && this.IsNearby((int)targetMate[5], (int)targetMate[6]))
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
                            targetMate = this.ts.QueryP("lifeform", (long)targetMate[1], (long)targetMate[2], (long)targetMate[3], (int)targetMate[4], typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
                            if (targetMate != null)
                            {
                                this.Move((int)targetMate[5], (int)targetMate[6]);
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
                    this.Life -= 6;
                }
                Thread.Sleep(50 - this.Speed);
            }
            this.GetCurrentLifeform(typeof(int), typeof(int));
        }
        private void Roam()
        {
            if (this.X == this.RoamX && this.Y == this.RoamY)
            {
                this.RoamX = (this.rng.Next() % (this.width - 3)) + 1;
                this.RoamY = (this.rng.Next() % (this.height - 3)) + 1;
            }
            this.Move(this.RoamX, this.RoamY);
        }
        private ITuple GetCurrentLifeform(object x, object y)
        {
            return this.ts.Get("lifeform", this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, x, y, this.Generation, this.VisualRange, this.MaxNrChildren, this.Speed);
        }
        private bool IsNearby(int x, int y)
        {
            return Math.Abs(this.X - x) <= 1 && Math.Abs(this.Y - y) <= 1;
        }
        private void Eat(ITuple food)
        {
            if (this.ts.GetP("food", food[1], typeof(int), food[3], food[4]) != null)
            {
                int amount = (int)food[1];
                this.Food += amount;
                if (this.nrChildren < this.MaxNrChildren)
                {
                    this.Life = Math.Min(this.Life + this.InitialLife / 4, this.InitialLife);
                }
                this.ts.Put("foodEaten");
            }
        }
        private ITuple FindMate()
        {
            IEnumerable<ITuple> targetmates = this.ts.QueryAll("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            targetmates = targetmates.Where(lf => this.CanSee((int)lf[5], (int)lf[6]) && this.CanBreed(lf));
            if (targetmates.Count() > 0)
            {
                return targetmates.ElementAt(rng.Next() % targetmates.Count());
            }
            return null;
        }
        private ITuple FindFood()
        {
            IEnumerable<ITuple> targetFoods = this.ts.QueryAll("food", typeof(int), typeof(int), typeof(int), typeof(int));
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
            this.ts.Put("lifeform", this.Genom, this.P1Genom, this.P2Genom, this.InitialLife, this.X, this.Y, this.Generation, this.VisualRange, this.MaxNrChildren, this.Speed);
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
            ITuple adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X, this.Y + 1, typeof(int), typeof(int), typeof(int), typeof(int));
            if (adjacent == null)
            {
                adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X + 1, this.Y, typeof(int), typeof(int), typeof(int), typeof(int));
                if (adjacent == null)
                {
                    adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X - 1, this.Y, typeof(int), typeof(int), typeof(int), typeof(int));
                    if (adjacent == null)
                    {
                        adjacent = this.ts.QueryP("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), this.X, this.Y - 1, typeof(int), typeof(int), typeof(int), typeof(int));
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
                int otherVisualRange = (int)other[8];
                int othermaxNrChildren = (int)other[9];
                int otherSpeed = (int)other[10];
                long genom = 31 + this.Genom;
                genom = (genom * 31) + otherGenom;
                int initiallife = (this.InitialLife + otherInitialLife) / 2;
                initiallife += (this.rng.Next() % initiallife) - (initiallife / 2);
                int food = 0;
                x = x == this.width ? this.X - 1 : X;
                y = y == this.width ? this.Y - 1 : Y;
                int generation = Math.Max(this.Generation, otherGeneration);
                int visualRange = (this.VisualRange + otherVisualRange) / 2;
                visualRange += (this.rng.Next() % visualRange) - (visualRange / 2);
                int maxNrChildren = ((this.MaxNrChildren + othermaxNrChildren) / 2) + (this.rng.Next() % 5) - 2;
                int speed = Math.Max(this.Speed, otherSpeed) + (this.rng.Next() % 5) - 2;
                this.ts.Put("spawn", genom, this.Genom, otherGenom, initiallife, food, x, y, generation + 1, visualRange, maxNrChildren, speed);
                this.nrChildren++;
            }
        }
    }
}
