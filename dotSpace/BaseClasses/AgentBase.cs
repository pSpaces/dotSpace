using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotSpace.BaseClasses
{
    public abstract class Agent
    {
        private Thread thread;
        protected string name;
        protected ITupleSpace ts;

        public Agent(string name, ITupleSpace ts)
        {
            this.thread = new Thread(this.DoWork);
            this.name = name;
            this.ts = ts;
        }

        public void Start()
        {
            this.thread.Start();
        }

        public void Join()
        {
            this.thread.Join();
        }

        public abstract void DoWork();


    }
}
