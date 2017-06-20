using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;

namespace Example7
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
                    t[0] = this.name;
                    t[1] = (int)t[1] + 1;
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
