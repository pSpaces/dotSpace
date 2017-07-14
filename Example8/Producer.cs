using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;

namespace Example8
{
    public class Producer : AgentBase
    {

        // This constructor records the name of the agent
        public Producer(string name, ISpace ts) : base(name, ts)
        {
        }

        // This is the function invoked when the agent starts running in a node
        protected override void DoWork()
        {
            try
            {
                Console.WriteLine(name + " adding items to the grocery list...");
                Console.WriteLine(name + " adding milk(1)");
                this.Put("milk", 1, "food");
                Console.WriteLine(name + " adding soap(2)");
                this.Put("soap", 2, "drug");
                Console.WriteLine(name + " adding butter(3)");
                this.Put("butter", 3, "food");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }


}
