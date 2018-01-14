using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using System;
using System.IO;
using System.Net.Sockets;

namespace dotSpace.Objects.Network.Protocols
{
    /// <summary>
    /// This classes represents a wrapper for a TCP socket, allowing it to encode and decode the stringbased messages using a provided encoder.
    /// </summary>
    internal sealed class Tcp : ProtocolBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private TcpClient client;
        private NetworkStream netStream;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the Tcp class.
        /// </summary>
        public Tcp(TcpClient client)
        {
            this.client = client;
            this.netStream = client.GetStream();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Reads a single stringbased message from the underlying Tcpclient, and returns the decoded message.
        /// </summary>
        public override IMessage Receive(IEncoder encoder)
        {
            try
            {
                return (MessageBase)encoder.Decode(netStream);
            }
            catch (Exception e)
            {
                this.client.Close();
            }
            return null;
        }

        /// <summary>
        /// Encodes the message and writes it to the underlying tcpclient as a stringbased message.
        /// </summary>
        public override void Send(IMessage message, IEncoder encoder)
        {
            try
            {
                encoder.Encode(netStream, message);
            }
            catch (Exception e)
            {
                this.client.Close();
            }
        }

        /// <summary>
        /// Closes the underlying tcpclient.
        /// </summary>
        public override void Close()
        {
            if (this.client.Connected)
            {
                this.client.Close();
            }
        }

        #endregion
    }
}
