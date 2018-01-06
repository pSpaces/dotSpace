using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Network;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Protocols;
using System;
using System.Net.Sockets;
using System.Threading;

namespace dotSpace.Objects.Network.Gates
{
    /// <summary>
    /// Provides mechanisms for listening of incoming UDP messages, and delegates the clientinformation back through a callback function.
    /// This class has not been fully implemented and as such UDP is not supported.
    /// </summary>
    internal sealed class UdpGate : GateBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private UdpClient listener;
        private bool listening;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the UdpGate class.
        /// </summary>
        public UdpGate(IEncoder encoder, ConnectionString connectionString) : base(encoder, connectionString)
        {
            throw new Exception("The UDP gate is not supported.");
            this.listener = new UdpClient();
            this.listening = false;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Initializes a udpclient with the specified host and port. The callback function is then invoked with the initialized client.
        /// </summary>
        public override void Start(Action<IConnectionMode> callback)
        {
            if (!this.listening)
            {
                this.listening = true;
                IConnectionMode mode = this.GetMode(this.ConnectionString.Mode, new Udp(this.ConnectionString.Host, this.ConnectionString.Port));
                new Thread(() => { callback(mode); }).Start();
            }
        }

        /// <summary>
        /// Allows the udp gate to reinitialize.
        /// </summary>
        public override void Stop()
        {
            this.listening = false;
        }

        #endregion
    }
}
