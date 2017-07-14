using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;

namespace Example6
{
    public class Philosopher : AgentBase
    {
        private int seatIndex;
        private int leftId;
        private int rightId;

        public Philosopher(string name, int seatIndex, int max, ISpace ts) : base(name, ts)
        {
            this.seatIndex = seatIndex;
            this.leftId = seatIndex;
            this.rightId = seatIndex == max ? 1 : seatIndex + 1;
        }

        protected override void DoWork()
        {
            ITuple lf, rf;
            try
            {
                while (true)
                {
                    // Take the left fork.
                    lf = this.Get("FORK", this.leftId);

                    // Try to take the right fork.
                    rf = this.GetP("FORK", this.rightId);
                    
                    // If we got the right fork, then eat and put back the forks.
                    if (rf != null)
                    {
                        Console.WriteLine(this.name + ": I AM EATING WITH BOTH MY HANDS: " + this.seatIndex);
                        this.Put(rf);
                        this.Put(lf);
                        Console.WriteLine("Done eating: " + this.seatIndex);
                    }
                    // Otherwise put back the left.
                    else
                    {
                        this.Put(lf);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}
