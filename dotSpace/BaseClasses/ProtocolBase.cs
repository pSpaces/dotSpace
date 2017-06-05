using dotSpace.Objects;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;
using System.Net;

namespace dotSpace.BaseClasses
{
    public abstract class ProtocolBase
    {
        protected TSOperationMap operationMap;
        protected NodeBase node;

        public ProtocolBase(NodeBase node)
        {
            this.node = node;
            if (this.node is ServerNode)
            {
                this.operationMap = new TSOperationMap((ServerNode)this.node);
            }
        }

        public abstract void ProcessRequest(ServerSocket socket, BasicRequest request);
        public abstract T PerformRequest<T>(IPEndPoint endpoint, BasicRequest request) where T : BasicResponse;

        protected T ValidateResponse<T>(MessageBase message) where T : BasicResponse
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

        protected T ValidateRequest<T>(MessageBase message) where T : BasicRequest
        {
            if (message is BasicRequest)
            {
                return message as T;
            }
            throw new Exception(string.Format("Unknown request"));
        }
    }
}
