using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Network.ConnectionModes;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using dotSpace.Objects.Network.Protocols;
using dotSpace.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    /// <summary>
    /// Provides networked access to a remote space, using any supported protocol and mode.
    /// </summary>
    public sealed class RemoteSpace : ISpace
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IConnectionMode mode;
        private IEncoder encoder;
        private ConnectionString connectionString;
        private ITupleFactory tupleFactory;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the RemoteSpace class providing networked access to a space repository. All tuples will be created using the provided tuple factory;
        /// if none is provided the default TupleFactory will be used.
        /// </summary>
        public RemoteSpace(string uri, ITupleFactory tuplefactory = null)
        {
            this.connectionString = new ConnectionString(uri);
            this.encoder = new RequestEncoder();
            this.tupleFactory = tuplefactory ?? new TupleFactory();
            if (string.IsNullOrEmpty(this.connectionString.Target))
            {
                throw new Exception("Must specify valid target.");
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(IPattern pattern)
        {
            return this.Get(pattern.Fields);
        }

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(params object[] pattern)
        {
            GetRequest request = new GetRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, pattern);
            GetResponse response = this.GetMode()?.PerformRequest<GetResponse>(request);
            return response.Result == null ? null : this.tupleFactory.Create(response.Result);
        }

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(IPattern pattern)
        {
            return this.GetP(pattern.Fields);
        }

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(params object[] pattern)
        {
            GetPRequest request = new GetPRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, pattern);
            GetPResponse response = this.GetMode()?.PerformRequest<GetPResponse>(request);
            return response.Result == null ? null : this.tupleFactory.Create(response.Result);
        }

        /// <summary>
        /// Retrieves and removes all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.GetAll(pattern.Fields);
        }

        /// <summary>
        /// Retrieves and removes all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(params object[] pattern)
        {
            GetAllRequest request = new GetAllRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, pattern);
            GetAllResponse response = this.GetMode()?.PerformRequest<GetAllResponse>(request);
            return response.Result == null ? null : response.Result.Select(x => this.tupleFactory.Create(x));
        }

        /// <summary>
        /// Retrieves the first tuple from the space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(IPattern pattern)
        {
            return this.Query(pattern.Fields);
        }

        /// <summary>
        /// Retrieves the first tuple from the space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(params object[] pattern)
        {
            QueryRequest request = new QueryRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, pattern);
            QueryResponse response = this.GetMode()?.PerformRequest<QueryResponse>(request);
            return response.Result == null ? null : this.tupleFactory.Create(response.Result);
        }

        /// <summary>
        /// Retrieves the first tuple from the space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(IPattern pattern)
        {
            return this.QueryP(pattern.Fields);
        }

        /// <summary>
        /// Retrieves the first tuple from the space, matching the specified pattern.The operation is non-blocking.The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(params object[] pattern)
        {
            QueryPRequest request = new QueryPRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, pattern);
            QueryPResponse response = this.GetMode()?.PerformRequest<QueryPResponse>(request);
            return response.Result == null ? null : this.tupleFactory.Create(response.Result);
        }

        /// <summary>
        /// Retrieves all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.QueryAll(pattern.Fields);
        }

        /// <summary>
        /// Retrieves all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(params object[] pattern)
        {
            QueryAllRequest request = new QueryAllRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, pattern);
            QueryAllResponse response = this.GetMode()?.PerformRequest<QueryAllResponse>(request);
            return response.Result == null ? null : response.Result.Select(x => this.tupleFactory.Create(x));
        }

        /// <summary>
        /// Inserts the tuple passed as argument into the space.
        /// </summary>
        public void Put(ITuple tuple)
        {
            this.Put(tuple.Fields);
        }

        /// <summary>
        /// Inserts the tuple passed as argument into the space.
        /// </summary>
        public void Put(params object[] tuple)
        {
            PutRequest request = new PutRequest(this.GetSource(), this.GetSessionId(), this.connectionString.Target, tuple);
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
            switch (this.connectionString.Mode)
            {
                case ConnectionMode.KEEP: lock (this.connectionString) { this.mode = this.mode ?? new Keep(this.GetProtocol(), this.encoder); } break;
                case ConnectionMode.CONN: return new Conn(this.GetProtocol(), this.encoder);
                case ConnectionMode.PUSH: return new Push(this.GetProtocol(), this.encoder);
                case ConnectionMode.PULL: return new Pull(this.GetProtocol(), this.encoder);
                default: return null;
            }
            return this.mode;
        }
        private IProtocol GetProtocol()
        {
            switch (this.connectionString.Protocol)
            {
                case Protocol.TCP: return new Tcp(new TcpClient(this.connectionString.Host, this.connectionString.Port));
                case Protocol.UDP: return new Udp(this.connectionString.Host, this.connectionString.Port);
                default: return null;
            }
        }

        #endregion
    }
}
