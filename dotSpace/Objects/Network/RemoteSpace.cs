using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.ConnectionModes;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using dotSpace.Objects.Network.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class RemoteSpace : ISpace
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IConnectionMode mode;
        private IEncoder encoder;
        private GateInfo gateInfo;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public RemoteSpace(string uri)
        {
            this.gateInfo = new GateInfo(uri);
            this.encoder = new RequestEncoder();
            if (string.IsNullOrEmpty(this.gateInfo.Target))
            {
                throw new Exception("Must specify valid target.");
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public ITuple Get(IPattern pattern)
        {
            return this.Get(pattern.Fields);
        }
        public ITuple Get(params object[] pattern)
        {
            GetRequest request = new GetRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, pattern);
            GetResponse response = this.GetMode()?.PerformRequest<GetResponse>(request);
            return response.Result == null ? null : new Objects.Spaces.Tuple(response.Result);
        }
        public ITuple GetP(IPattern pattern)
        {
            return this.GetP(pattern.Fields);
        }
        public ITuple GetP(params object[] pattern)
        {
            GetPRequest request = new GetPRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, pattern);
            GetPResponse response = this.GetMode()?.PerformRequest<GetPResponse>(request);
            return response.Result == null ? null : new Objects.Spaces.Tuple(response.Result);
        }
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.GetAll(pattern.Fields);
        }
        public IEnumerable<ITuple> GetAll(params object[] pattern)
        {
            GetAllRequest request = new GetAllRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, pattern);
            GetAllResponse response = this.GetMode()?.PerformRequest<GetAllResponse>(request);
            return response.Result == null ? null : response.Result.Select(x => new Objects.Spaces.Tuple(x));
        }
        public ITuple Query(IPattern pattern)
        {
            return this.Query(pattern.Fields);
        }
        public ITuple Query(params object[] pattern)
        {
            QueryRequest request = new QueryRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, pattern);
            QueryResponse response = this.GetMode()?.PerformRequest<QueryResponse>(request);
            return response.Result == null ? null : new Objects.Spaces.Tuple(response.Result);
        }
        public ITuple QueryP(IPattern pattern)
        {
            return this.QueryP(pattern.Fields);
        }
        public ITuple QueryP(params object[] pattern)
        {
            QueryPRequest request = new QueryPRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, pattern);
            QueryPResponse response = this.GetMode()?.PerformRequest<QueryPResponse>(request);
            return response.Result == null ? null : new Objects.Spaces.Tuple(response.Result);
        }
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.QueryAll(pattern.Fields);
        }
        public IEnumerable<ITuple> QueryAll(params object[] pattern)
        {
            QueryAllRequest request = new QueryAllRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, pattern);
            QueryAllResponse response = this.GetMode()?.PerformRequest<QueryAllResponse>(request);
            return response.Result == null ? null : response.Result.Select(x => new Objects.Spaces.Tuple(x));
        }
        public void Put(ITuple tuple)
        {
            this.Put(tuple.Fields);
        }
        public void Put(params object[] tuple)
        {
            PutRequest request = new PutRequest(this.GetSource(), this.GetSessionId(), this.gateInfo.Target, tuple);
            this.GetMode()?.PerformRequest<PutResponse>(request);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private string GetSessionId()
        {
            return Guid.NewGuid().ToString();
        }
        private string GetSource()
        {
            return string.Empty;
        }
        private IConnectionMode GetMode()
        {
            switch (this.gateInfo.Mode)
            {
                case ConnectionMode.KEEP: lock (this.gateInfo) { this.mode = this.mode ?? new Keep(this.GetProtocol(), this.encoder); } break;
                case ConnectionMode.CONN: return new Conn(this.GetProtocol(), this.encoder);
                case ConnectionMode.PULL: return new Conn(this.GetProtocol(), this.encoder);
                case ConnectionMode.PUSH: return new Conn(this.GetProtocol(), this.encoder);
                default: return null;
            }
            return this.mode;
        }
        private IProtocol GetProtocol()
        {
            switch (this.gateInfo.Protocol)
            {
                case Protocol.TCP: return new Tcp(new TcpClient(this.gateInfo.Host, this.gateInfo.Port));
                case Protocol.UDP: return new Udp(new UdpClient(), this.gateInfo.Host, this.gateInfo.Port);
                default: return null;
            }
        }

        #endregion
    }
}
