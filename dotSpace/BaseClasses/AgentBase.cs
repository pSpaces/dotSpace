using dotSpace.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotSpace.BaseClasses
{
    public abstract class AgentBase : ISpace
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected string name;
        protected ISpace space;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public AgentBase(string name, ISpace space)
        {
            this.name = name;
            this.space = space;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public void Start()
        {
            var t = Task.Factory.StartNew(this.DoWork);
        }
        public ITuple Get(IPattern pattern)
        {
            return this.space.Get(pattern);
        }
        public ITuple Get(params object[] pattern)
        {
            return this.space.Get(pattern);
        }
        public ITuple GetP(IPattern pattern)
        {
            return this.space.GetP(pattern);
        }
        public ITuple GetP(params object[] pattern)
        {
            return this.space.GetP(pattern);
        }
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.space.GetAll(pattern);
        }
        public IEnumerable<ITuple> GetAll(params object[] pattern)
        {
            return this.space.GetAll(pattern);
        }
        public ITuple Query(IPattern pattern)
        {
            return this.space.Query(pattern);
        }
        public ITuple Query(params object[] pattern)
        {
            return this.space.Query(pattern);
        }
        public ITuple QueryP(IPattern pattern)
        {
            return this.space.QueryP(pattern);
        }
        public ITuple QueryP(params object[] pattern)
        {
            return this.space.QueryP(pattern);
        }
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.space.QueryAll(pattern);
        }
        public IEnumerable<ITuple> QueryAll(params object[] pattern)
        {
            return this.space.QueryAll(pattern);
        }
        public void Put(ITuple tuple)
        {
            this.space.Put(tuple);
        }
        public void Put(params object[] tuple)
        {
            this.space.Put(tuple);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected abstract void DoWork(); 

        #endregion
    }
}
