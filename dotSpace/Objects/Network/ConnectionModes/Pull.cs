using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Network;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;

namespace dotSpace.Objects.Network.ConnectionModes
{
    /// <summary>
    /// Implements the mechanisms to support the PULL connection scheme.
    /// This connection mode is incomplete, and is thus not supported.
    /// </summary>
    public sealed class Pull : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the Pull class. 
        /// </summary>
        public Pull(IProtocol protocol, IEncoder encoder) : base(protocol, encoder)
        {
            throw new Exception("The Pull connection scheme is not supported.");
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Not implemented.
        /// </summary>
        public override void ProcessRequest(IOperationMap operationMap)
        {
        }
        /// <summary>
        /// Not implemented.
        /// </summary>
        public override T PerformRequest<T>(IMessage request)
        {
            return default(T);
        }

        #endregion
    }
}