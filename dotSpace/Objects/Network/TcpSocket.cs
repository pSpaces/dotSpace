using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.IO;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class TcpSocket : SocketBase
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

        public TcpSocket(TcpClient client)
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

        public override MessageBase Receive(IEncoder encoder)
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
        public override void Send(MessageBase message, IEncoder encoder)
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
