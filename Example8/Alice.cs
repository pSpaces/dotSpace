using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;

namespace Example8
{
    public class Alice : AgentBase
    {
        public Alice(string name, ISpace ts) : base(name, ts)
        {
        }

        // This is the function invoked when the agent starts running in a node
        protected override void DoWork()
        {
            try
            {
                Console.WriteLine(name + " adding items to the grocery list...");
                Console.WriteLine(name + " adding one bottle(s) of milk");
                this.Put("milk", 1);
                Console.WriteLine(name + " adding one piece of soap");
                this.Put("soap", 2);
                Console.WriteLine(name + " adding three piecess of butter");
                this.Put("butter", 3);
                Console.WriteLine(name + " go!");
                this.Put("shop!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }


}
