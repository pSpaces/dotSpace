using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.ConnectionModes;
using System;

namespace dotSpace.BaseClasses.Network
{
    /// <summary>
    /// Provides basic functionality for defining the typical behavior of a Gate. This is an abstract class.
    /// </summary>
    public abstract class GateBase : IGate
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

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
            this.ConnectionString = connectionString;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Property based representation of the uri endpoint.
        /// </summary>
        public ConnectionString ConnectionString { get; }

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
                case ConnectionMode.KEEP: return new Keep(protocol, this.encoder);
                case ConnectionMode.CONN: return new Conn(protocol, this.encoder);
                case ConnectionMode.PUSH: return new Push(protocol, this.encoder);
                case ConnectionMode.PULL: return new Pull(protocol, this.encoder);
                default: return null;
            }
        } 

        #endregion
    }
}
