using dotSpace.Interfaces;
using System.Threading;

namespace dotSpace.BaseClasses
{
    public abstract class AgentBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private Thread thread;
        protected string name;
        protected ISpace ts;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public AgentBase(string name, ISpace ts)
        {
            this.thread = new Thread(this.DoWork);
            this.name = name;
            this.ts = ts;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

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
