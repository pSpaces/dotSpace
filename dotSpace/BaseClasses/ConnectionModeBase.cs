﻿using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;
using System.Net;

namespace dotSpace.BaseClasses
{
    public abstract class ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected ISocket socket;
        protected IEncoder encoder;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ConnectionModeBase(ISocket socket, IEncoder encoder)
        {
            this.socket = socket;
            this.encoder = encoder;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public abstract void ProcessRequest(OperationMap operationMap);
        public abstract T PerformRequest<T>(BasicRequest request) where T : BasicResponse;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected BasicResponse ValidateResponse(MessageBase message)
        {
            if (message is BasicResponse)
            {
                BasicResponse response = (BasicResponse)message;
                if (response.Code == StatusCode.OK)
                {
                    return (BasicResponse)message;
                }
                throw new Exception(string.Format("{0} - {1}", response.Code, response.Message));
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_RESPONSE, StatusMessage.BAD_RESPONSE));
        }
        protected BasicRequest ValidateRequest(MessageBase message)
        {
            if (message is BasicRequest)
            {
                return (BasicRequest)message;
            }
            throw new Exception(string.Format("{0} - {1}", StatusCode.BAD_REQUEST, StatusMessage.BAD_REQUEST));
        } 

        #endregion
    }
}