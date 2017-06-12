using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Gates;
using System.Collections.Generic;

namespace dotSpace.BaseClasses
{
    public abstract class RepositoryBase : IRepository
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

        public void AddGate(string uri)
        {
            IGate gate = this.gateFactory.CreateGate(uri, this.encoder);
            if (gate != null)
            {
                this.gates.Add(gate);
                gate.Start(this.OnConnect);
            }
        }
        public void AddSpace(string identifier, ISpace tuplespace)
        {
            if (!this.spaces.ContainsKey(identifier))
            {
                this.spaces.Add(identifier, tuplespace);
            }
        }
        public ISpace GetSpace(string target)
        {
            if (this.spaces.ContainsKey(target))
            {
                return this.spaces[target];
            }
            return null;
        }
        public ITuple Get(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.Get(pattern);
        }
        public ITuple Get(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.Get(pattern);
        }
        public ITuple GetP(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.GetP(pattern);
        }
        public ITuple GetP(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.GetP(pattern);
        }
        public IEnumerable<ITuple> GetAll(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.GetAll(pattern);
        }
        public IEnumerable<ITuple> GetAll(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.GetAll(pattern);
        }
        public ITuple Query(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.Query(pattern);
        }
        public ITuple Query(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.Query(pattern);
        }
        public ITuple QueryP(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.QueryP(pattern);
        }
        public ITuple QueryP(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.QueryP(pattern);
        }
        public IEnumerable<ITuple> QueryAll(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.QueryAll(pattern);
        }
        public IEnumerable<ITuple> QueryAll(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.QueryAll(pattern);
        }
        public void Put(string target, ITuple tuple)
        {
            this.GetSpace(target)?.Put(tuple);
        }
        public void Put(string target, params object[] tuple)
        {
            this.GetSpace(target)?.Put(tuple);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected abstract void OnConnect(IConnectionMode mode);

        #endregion
    }
}
