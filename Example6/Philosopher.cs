using dotSpace.BaseClasses;
using dotSpace.Interfaces;
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
                    // The get operation returns a tuple
                    lf = this.Get("FORK", this.leftId);
                    rf = this.GetP("FORK", this.rightId);
                    if (rf != null)
                    {
                        Console.WriteLine(this.name + ": I AM EATING WITH BOTH MY HANDS: " + this.seatIndex);
                        this.Put(rf);
                        this.Put(lf);
                        Console.WriteLine("Done eating: " + this.seatIndex);
                    }
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
