using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects;
using System;

namespace Example4
{
    public class Charlie : AgentBase
    {
        public Charlie(string who, ISpace ts) : base(who, ts)
        {
        }

        protected override void DoWork()
        {
            Pattern what = new Pattern(typeof(string), typeof(int));
            Pattern go = new Pattern("shop!");
            ITuple t;
            try
            {
                if (this.QueryP(go) != null)
                {
                    while (true)
                    {
                        t = this.Get(what);
                        Console.WriteLine(name + " shopping " + t[1] + " units of " + t[0] + "...");
                    }
                }
                else
                {
                    Console.WriteLine(name + " relaxing...");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

            }
        }

    }


}
