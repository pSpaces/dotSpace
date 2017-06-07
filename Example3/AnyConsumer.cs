﻿using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects;
using System;

namespace Example3
{

    public class AnyConsumer : Agent
    {

        public AnyConsumer(string name, ITupleSpace ts) : base(name, ts)
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
                    t = this.ts.Get(what);
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
