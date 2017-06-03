using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Collections.Generic;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public class ServerNode : NodeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields
        private TcpListener listener;
        private Dictionary<string, ITupleSpace> spaces;
        private Dictionary<ConnectionMode, Protocol> protocols;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ServerNode(int port, bool listenOnStart = false)
        {
            this.listener = new TcpListener(port);
            this.spaces = new Dictionary<string, ITupleSpace>();
            this.protocols = new Dictionary<ConnectionMode, Protocol>();
            this.protocols.Add(ConnectionMode.CONN, new ConnProtocol(this));
            this.protocols.Add(ConnectionMode.PUSH, new PushProtocol(this));
            if (listenOnStart)
            {
                this.StartListen();
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public ITupleSpace this[string indexer]
        {
            get
            {
                if (this.spaces.ContainsKey(indexer))
                {
                    return this.spaces[indexer];
                }
                return null;
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override ITuple Get(string identifier, IPattern pattern)
        {
            return this[identifier].Get(pattern);
        }
        public override ITuple GetP(string identifier, IPattern pattern)
        {
            return this[identifier].GetP(pattern);
        }
        public override ITuple Query(string identifier, IPattern pattern)
        {
            return this[identifier].Query(pattern);
        }
        public override ITuple QueryP(string identifier, IPattern pattern)
        {
            return this[identifier].QueryP(pattern);
        }
        public override void Put(string identifier, ITuple t)
        {
            this[identifier].Put(t);
        }
        public void StartListen()
        {
            this.listener.Start(this.OnClientConnect);
        }
        public void StopListen()
        {
            this.listener.Stop();
        }
        public void AddSpace(string identifier, ITupleSpace tuplespace)
        {
            if (!this.spaces.ContainsKey(identifier))
            {
                this.spaces.Add(identifier, tuplespace);
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override T Decode<T>(string msg) 
        {
            BasicRequest breq = msg.Deserialize<BasicRequest>();
            switch (breq.Action)
            {
                case ActionType.PUT_REQUEST: return msg.Deserialize<PutRequest>() as T;
                case ActionType.GET_REQUEST: return msg.Deserialize<GetRequest>() as T;
                case ActionType.QUERY_REQUEST: return msg.Deserialize<GetRequest>() as T;
            }

            return default(T);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private void OnClientConnect(TcpClient client)
        {
            BasicRequest request = this.Receive<BasicRequest>(client);
            //this.GetProtocol(request)?.Execute(client, request);
        }
        private Protocol GetProtocol(BasicRequest request)
        {
            if (this.protocols.ContainsKey(request.Mode))
            {
                return this.protocols[request.Mode];
            }

            return null;
        }

        #endregion
    }
}
