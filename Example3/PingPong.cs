using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;

namespace Example3
{
    public class PingPong : AgentBase
    {
        private string other;
        public PingPong(string name, string other, ISpace ts) : base(name, ts)
        {
            this.other = other;
        }
        protected override void DoWork()
        {
            try
            {
                while (true)
                {
                    // Retrieve the "ping" or "pong".
                    ITuple t = this.Get(this.other, typeof(int));

                    // Set the tuple values to our name, and increment the counter.
                    t[0] = this.name;
                    t[1] = (int)t[1]+1;

                    // Display the tuple, and put it back.
                    Console.WriteLine(t);
                    this.Put(t);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
