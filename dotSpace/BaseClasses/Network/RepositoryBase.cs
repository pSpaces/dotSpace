using System;
using dotSpace.Interfaces.Network;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Gates;
using System.Collections.Generic;
using System.Linq;

namespace dotSpace.BaseClasses.Network
{
    /// <summary>
    /// Provides the basic functionality for supporting multiple distributed spaces. This is an abstract class.
    /// The RepositoryBase class allows direct access to the contained spaces through their respective identifies.
    /// Additionally, RepositoryBase facilitates distributed access to the underlying spaces through Gates. 
    /// </summary>
    public abstract class RepositoryBase : IRepository, IDisposable
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected List<IGate> gates;
        protected IEncoder encoder;
        protected Dictionary<string, ISpace> spaces;
        protected GateFactory gateFactory;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the RepositoryBase class.
        /// </summary>
        public RepositoryBase()
        {
            this.spaces = new Dictionary<string, ISpace>();
            this.gates = new List<IGate>();
            this.encoder = new ResponseEncoder();
            this.gateFactory = new GateFactory();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Adds a new Gate to the repository based on the provided connectionstring.
        /// </summary>
        public void AddGate(string connectionstring)
        {
            IGate gate = this.gateFactory.CreateGate(connectionstring, this.encoder);
            if (gate != null)
            {
                this.gates.Add(gate);
                gate.Start(this.OnConnect);
            }
        }
        /// <summary>
        /// Closes the gate represented by the specific connectionstring, and terminates the underlying thread.
        /// </summary>
        public void CloseGate(string uri)
        {
            ConnectionString connectionString = new ConnectionString(uri);
            this.gates.FirstOrDefault(x => x.ConnectionString.Equals(connectionString))?.Stop();
        }
        /// <summary>
        /// Closes all gates, and terminates the underlying associated thread.
        /// </summary>
        public void Dispose()
        {
            foreach (IGate gate in this.gates)
            {
                gate.Stop();
            }
        }
        /// <summary>
        /// Adds a new Space to the repository, identified by the specified parameter.
        /// </summary>
        public void AddSpace(string identifier, ISpace tuplespace)
        {
            if (!this.spaces.ContainsKey(identifier))
            {
                this.spaces.Add(identifier, tuplespace);
            }
        }
        /// <summary>
        /// Returns the local instance of the space identified by the parameter.
        /// </summary>
        public ISpace GetSpace(string identifier)
        {
            if (this.spaces.ContainsKey(identifier))
            {
                return this.spaces[identifier];
            }
            return null;
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.Get(pattern);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.Get(pattern);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.GetP(pattern);
        }
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.GetP(pattern);
        }
        /// <summary>
        /// Retrieves and removes all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.GetAll(pattern);
        }
        /// <summary>
        /// Retrieves and removes all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.GetAll(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.Query(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.Query(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.QueryP(pattern);
        }
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern.The operation is non-blocking.The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.QueryP(pattern);
        }
        /// <summary>
        /// Retrieves all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.QueryAll(pattern);
        }
        /// <summary>
        /// Retrieves all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.QueryAll(pattern);
        }
        /// <summary>
        /// Inserts the tuple passed as argument into the target Space.
        /// </summary>
        public void Put(string target, ITuple tuple)
        {
            this.GetSpace(target)?.Put(tuple);
        }
        /// <summary>
        /// Inserts the tuple passed as argument into the target Space.
        /// </summary>
        public void Put(string target, params object[] tuple)
        {
            this.GetSpace(target)?.Put(tuple);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Template method that is called when the repository receives an incoming connection.
        /// </summary>
        protected abstract void OnConnect(IConnectionMode mode);

        #endregion
    }
}
