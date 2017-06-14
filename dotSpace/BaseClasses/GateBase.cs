using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.ConnectionModes;
using System;

namespace dotSpace.BaseClasses
{
    /// <summary>
    /// Provides basic functionality for defining the typical behavior of a Gate. This is an abstract class.
    /// </summary>
    public abstract class GateBase : IGate
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected ConnectionString connectionString;
        protected IEncoder encoder;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GateBase class.
        /// </summary>
        public GateBase(IEncoder encoder, ConnectionString connectionString)
        {
            this.encoder = encoder;
            this.connectionString = connectionString;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Starts the process controlling the gate. If a connection is received the passed callback
        /// function is executed.
        /// </summary>
        public abstract void Start(Action<IConnectionMode> callback);
        /// <summary>
        /// Stops the gate from reacting on incoming connections.
        /// </summary>
        public abstract void Stop();
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods
        
        /// <summary>
        /// Factorymethod returning a connectionmode object based on the specified parameters.
        /// </summary>
        protected IConnectionMode GetMode(ConnectionMode connectionmode, IProtocol protocol)
        {
            switch (connectionmode)
            {
                case ConnectionMode.CONN: return new Conn(protocol, this.encoder);
                case ConnectionMode.KEEP: return new Keep(protocol, this.encoder);
                case ConnectionMode.PULL: return null;
                case ConnectionMode.PUSH: return null;
                default: return null;
            }
        } 

        #endregion
    }
}
