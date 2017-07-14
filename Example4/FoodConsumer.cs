using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using System;
using System.Collections.Generic;

namespace Example4
{
    public class FoodConsumer : AgentBase
    {
        private int itemCnt;
        private List<ITuple> groceries;

        public FoodConsumer(string name, ISpace ts) : base(name, ts)
        {
            this.itemCnt = 0;
            this.groceries = new List<ITuple>();
        }

        protected override void DoWork()
        {
            // Note how templates are created in dotSpace
            Pattern what = new Pattern(typeof(string), typeof(int), "food");
            
            // The tuple is necessary to capture the result of a get operation
            ITuple t;
            try
            {
                while (true)
                {
                    bool goShop = false;

                    // The get operation returns a tuple, that we save into t
                    t = this.GetP(what);
                    if (t != null)
                    {
                        // The extracted field needs often to be casted to the right class
                        this.itemCnt += (int)t[1];
                        this.groceries.Add(t);
                    }
                    else
                    {
                        goShop = true;
                    }

                    // Note how the fields of the tuple t are accessed
                    if (this.itemCnt >= 3 || (goShop && this.itemCnt > 0))
                    {
                        Console.WriteLine("GOING SHOPPING: ");
                        foreach (ITuple item in this.groceries)
                        {
                            Console.WriteLine(name + " shopping " + item[1] + " units of " + item[0] + "...");
                        }

                        this.itemCnt = 0;
                        this.groceries.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }


}
