using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Protocols;
using System.Collections.Generic;

namespace dotSpace.Objects.Network
{
    public sealed class SpaceRepository : RepositoryBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private Dictionary<string, ISpace> spaces;
        private OperationMap operationMap;
        private List<IGate> gates;
        private IEncoder encoder;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public SpaceRepository() : base()
        {
            this.encoder = new ResponseEncoder();
            this.spaces = new Dictionary<string, ISpace>();
            this.gates = new List<IGate>();
            this.operationMap = new OperationMap(this);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public void AddGate(string uri)
        {
            GateInfo gateInfo = new GateInfo(uri);
            IGate gate = null;
            switch (gateInfo.Protocol)
            {
                case "tcp": gate = new TcpGate(gateInfo); break;
                default: break;
            }
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
        public override ISpace GetSpace(string target)
        {
            if (this.spaces.ContainsKey(target))
            {
                return this.spaces[target];
            }
            return null;
        }
        public override ITuple Get(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.Get(pattern);
        }
        public override ITuple Get(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.Get(pattern);
        }
        public override ITuple GetP(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.GetP(pattern);
        }
        public override ITuple GetP(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.GetP(pattern);
        }
        public override IEnumerable<ITuple> GetAll(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.GetAll(pattern);
        }
        public override IEnumerable<ITuple> GetAll(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.GetAll(pattern);
        }
        public override ITuple Query(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.Query(pattern);
        }
        public override ITuple Query(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.Query(pattern);
        }
        public override ITuple QueryP(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.QueryP(pattern);
        }
        public override ITuple QueryP(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.QueryP(pattern);
        }
        public override IEnumerable<ITuple> QueryAll(string target, IPattern pattern)
        {
            return this.GetSpace(target)?.QueryAll(pattern);
        }
        public override IEnumerable<ITuple> QueryAll(string target, params object[] pattern)
        {
            return this.GetSpace(target)?.QueryAll(pattern);
        }
        public override void Put(string target, ITuple tuple)
        {
            this.GetSpace(target)?.Put(tuple);
        }
        public override void Put(string target, params object[] tuple)
        {
            this.GetSpace(target)?.Put(tuple);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        // move to gatebase
        private void OnConnect(ISocket client, ConnectionMode mode)
        {
            this.GetMode(mode, client)?.ProcessRequest(this.operationMap);
        }
        private ConnectionModeBase GetMode(ConnectionMode connectionmode, ISocket socket)
        {
            switch (connectionmode)
            {
                case ConnectionMode.CONN: return new Conn(socket, this.encoder);
                case ConnectionMode.KEEP: return new Keep(socket, this.encoder);
                case ConnectionMode.PULL: return null;
                case ConnectionMode.PUSH: return null;
                default: return null;
            }
        }

        #endregion
    }
}
