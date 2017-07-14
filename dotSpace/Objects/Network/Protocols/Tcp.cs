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
        private StreamReader reader;
        private StreamWriter writer;

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
            this.reader = new StreamReader(this.netStream);
            this.writer = new StreamWriter(this.netStream);
            this.writer.AutoFlush = true;
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
                string msg = reader.ReadLine();
                return (MessageBase)encoder.Decode(msg);
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
                string msg = encoder.Encode(message);
                writer.WriteLine(msg);
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
