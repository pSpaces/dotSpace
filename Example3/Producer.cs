using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;

namespace Example3
{
    public class Producer : Agent
    {

        // This constructor records the name of the agent
        public Producer(string name, ITupleSpace ts) : base(name, ts)
        {
        }

        // This is the function invoked when the agent starts running in a node
        protected override void DoWork()
        {
            try
            {
                Console.WriteLine(name + " adding items to the grocery list...");
                Console.WriteLine(name + " adding milk(1)");
                this.ts.Put("milk", 1, "food");
                Console.WriteLine(name + " adding soap(2)");
                this.ts.Put("soap", 2, "drug");
                Console.WriteLine(name + " adding butter(3)");
                this.ts.Put("butter", 3, "food");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }


}
