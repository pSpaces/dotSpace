using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;

namespace dotSpace.Objects.Network
{
    public class ClientNode : NodeBase
    {
        private ConnectionMode mode;
        private string address;
        private int port;

        public ClientNode(ConnectionMode mode, string address, int port)
        {
            this.mode = mode;
            this.address = address;
            this.port = port;
        }
        public override ITuple Get(string target, IPattern pattern)
        {
            GetRequest request = new GetRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            ClientSocket socket = this.GetSocket();
            socket.Send(request);
            MessageBase message = socket.Receive<GetResponse>();
            GetResponse response = this.ValidateMessage<GetResponse>(message);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override ITuple GetP(string target, IPattern pattern)
        {
            GetPRequest request = new GetPRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            ClientSocket socket = this.GetSocket();
            socket.Send(request);
            MessageBase message = socket.Receive<GetPResponse>();
            GetPResponse response = this.ValidateMessage<GetPResponse>(message);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override ITuple Query(string target, IPattern pattern)
        {
            QueryRequest request = new QueryRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            ClientSocket socket = this.GetSocket();
            socket.Send(request);
            MessageBase message = socket.Receive<QueryResponse>();
            QueryResponse response = this.ValidateMessage<QueryResponse>(message);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override ITuple QueryP(string target, IPattern pattern)
        {
            QueryPRequest request = new QueryPRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            ClientSocket socket = this.GetSocket();
            socket.Send(request);
            MessageBase message = socket.Receive<QueryPResponse>();
            QueryPResponse response = this.ValidateMessage<QueryPResponse>(message);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override void Put(string target, ITuple tuple)
        {
            PutRequest request = new PutRequest(this.mode, this.GetSource(), this.GetSessionId(), target, tuple);
            ClientSocket socket = this.GetSocket();
            socket.Send(request);
            MessageBase message = socket.Receive<PutResponse>();
            PutResponse response = this.ValidateMessage<PutResponse>(message);
        }

        private string GetSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetSource()
        {
            return this.GetLocalIPAddress();
        }

        private ClientSocket GetSocket()
        {
            return new ClientSocket(this.CreateEndpoint(this.address, this.port));
        }

        private T ValidateMessage<T>(MessageBase message) where T : BasicResponse
        {
            if (message is BasicResponse)
            {
                BasicResponse response = (BasicResponse)message;
                if (response.Code == 200)
                {
                    return message as T;
                }
                throw new Exception(string.Format("{0} - {1}", response.Code, response.Message));
            }
            throw new Exception(string.Format("Unknown response"));
        }

    }
}
