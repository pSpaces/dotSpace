using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using System;

namespace Example8
{

    public class AnyConsumer : AgentBase
    {

        public AnyConsumer(string name, ISpace ts) : base(name, ts)
        {
        }

        protected override void DoWork()
        {
            // Note how patterns are created in in dotSpace            
            Pattern what = new Pattern(typeof(string), typeof(int), typeof(string));
            // The tuple is necessary to capture the result of a get operation
            ITuple t;
            try
            {
                while (true)
                {
                    // The get operation returns a tuple, that we save into t
                    t = this.Get(what);
                    // Note how the fields of the tuple t are accessed
                    Console.WriteLine(this.name + " shopping " + t[1] + " units of " + t[0] + "...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }

}
