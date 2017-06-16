using dotSpace.BaseClasses;
using dotSpace.Interfaces;
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
                    ITuple t = this.Get(this.other, typeof(int));
                    int value = (int)t[1] + 1;
                    Console.WriteLine(string.Format("{0}: {1}", this.name, value));
                    this.Put(this.name, value);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
