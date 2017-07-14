using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace dotSpace.BaseClasses.Space
{
    /// <summary>
    /// Provides basic functionality for a threaded interaction with a space. This is an abstract class.
    /// </summary>
    public abstract class AgentBase : ISpace
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected string name;
        protected ISpace space;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the AgentBase class.
        /// </summary>
        public AgentBase(string name, ISpace space)
        {
            this.name = name;
            this.space = space;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Starts the underlying thread, executing the 'DoWork' method.
        /// </summary>
        public void Start()
        {
            new Thread(this.DoWork).Start();
            //var t = Task.Factory.StartNew(this.DoWork);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(IPattern pattern)
        {
            return this.space.Get(pattern);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(params object[] pattern)
        {
            return this.space.Get(pattern);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(IPattern pattern)
        {
            return this.space.GetP(pattern);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(params object[] pattern)
        {
            return this.space.GetP(pattern);
        }
        /// <summary>
        /// Retrieves and removes all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.space.GetAll(pattern);
        }
        /// <summary>
        /// Retrieves and removes all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(params object[] pattern)
        {
            return this.space.GetAll(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(IPattern pattern)
        {
            return this.space.Query(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(params object[] pattern)
        {
            return this.space.Query(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(IPattern pattern)
        {
            return this.space.QueryP(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the Space, matching the specified pattern.The operation is non-blocking.The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(params object[] pattern)
        {
            return this.space.QueryP(pattern);
        }
        /// <summary>
        /// Retrieves all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.space.QueryAll(pattern);
        }
        /// <summary>
        /// Retrieves all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(params object[] pattern)
        {
            return this.space.QueryAll(pattern);
        }
        /// <summary>
        /// Inserts the tuple passed as argument into the Space.
        /// </summary>
        public void Put(ITuple tuple)
        {
            this.space.Put(tuple);
        }
        /// <summary>
        /// Inserts the tuple passed as argument into the Space.
        /// </summary>
        public void Put(params object[] tuple)
        {
            this.space.Put(tuple);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Method which is automatically executed when starting the agent.
        /// </summary>
        protected abstract void DoWork(); 

        #endregion
    }
}
