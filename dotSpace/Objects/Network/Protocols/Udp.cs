using dotSpace.BaseClasses.Network;
using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace dotSpace.Objects.Network.Protocols
{
    /// <summary>
    /// This classes represents a wrapper for a UDP socket, allowing it to encode and decode the stringbased messages using a provided encoder.
    /// This class has not been fully implemented and as such UDP is not supported.
    /// </summary>
    internal sealed class Udp : ProtocolBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private UdpClient client;
        private IPEndPoint endpoint;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the Udp class.
        /// </summary>
        public Udp(string host, int port)
        {
            throw new Exception("The UDP gate is not supported.");
            this.client = new UdpClient(port);            
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// This method has not been fully completed.
        /// Reads a single stringbased message from the underlying Tcpclient, and returns the decoded message.
        /// </summary>
        public override IMessage Receive(IEncoder encoder)
        {
            try
            {
                Byte[] receiveBytes = this.client.Receive(ref this.endpoint);
                string msg = Encoding.ASCII.GetString(receiveBytes);
                return (MessageBase)encoder.Decode(msg);
            }
            catch (Exception e)
            {
                this.client.Close();
            }
            return null;
        }

        /// <summary>
        /// This method has not been fully completed.
        /// Encodes the message and writes it to the underlying tcpclient as a stringbased message.
        /// </summary>
        public override void Send(IMessage message, IEncoder encoder)
        {
            try
            {
                string msg = encoder.Encode(message);
                byte[] data  = Encoding.ASCII.GetBytes(msg);
                this.client.Send(data, data.Length);
            }
            catch (Exception e)
            {
                this.client.Close();
            }
        }

        /// <summary>
        /// Closes the underlying udp socket.
        /// </summary>
        public override void Close()
        {

            this.client.Close();
        }

        #endregion
    }
}
