using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects;
using System;

namespace Example4
{
    public class Bob : Agent
    {
        public Bob(string name, ITupleSpace ts) : base(name, ts)
        {
        }

        protected override void DoWork()
        {
            Pattern what = new Pattern(typeof(string), typeof(int));
            Pattern go = new Pattern("shop!");
            ITuple t;
            try
            {
                this.ts.Query(go);
                while (true)
                {
                    // The get operation returns a tuple, that we save into t
                    t = this.ts.Get(what);
                    // Note how the fields of the tuple t are accessed
                    Console.WriteLine(name + " shopping " + t[1] + " units of " + t[0] + "...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

            }
        }

    }


}
