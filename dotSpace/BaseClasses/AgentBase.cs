using dotSpace.Interfaces;
using System.Threading;

namespace dotSpace.BaseClasses
{
    public abstract class Agent
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private Thread thread;
        protected string name;
        protected ITupleSpace ts;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Agent(string name, ITupleSpace ts)
        {
            this.thread = new Thread(this.DoWork);
            this.name = name;
            this.ts = ts;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // MyRegion

        public void Start()
        {
            this.thread.Start();
        }

        public void Join()
        {
            this.thread.Join();
        }

        protected abstract void DoWork();  

        #endregion
    }
}
