using dotSpace.Enumerations;
using dotSpace.Interfaces;
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
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected OperationMap operationMap;
        protected RepositoryBase repository;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ProtocolBase(RepositoryBase repository)
        {
            this.repository = repository;
            if (this.repository is SpaceRepository)
            {
                this.operationMap = new OperationMap((SpaceRepository)this.repository);
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public abstract void ProcessRequest(Socket socket, BasicRequest request);
        public abstract T PerformRequest<T>(IPEndPoint endpoint, IEncoder encoder, BasicRequest request) where T : BasicResponse;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected T ValidateResponse<T>(MessageBase message) where T : BasicResponse
        {
            if (message is BasicResponse)
            {
                BasicResponse response = (BasicResponse)message;
                if (response.Code == StatusCode.OK)
                {
                    return message as T;
                }
                throw new Exception(string.Format("{0} - {1}", response.Code, response.Message));
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_RESPONSE, StatusMessage.BAD_RESPONSE));
        }
        protected T ValidateRequest<T>(MessageBase message) where T : BasicRequest
        {
            if (message is BasicRequest)
            {
                return message as T;
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_REQUEST, StatusMessage.BAD_REQUEST));
        } 

        #endregion
    }
}
