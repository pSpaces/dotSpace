using dotSpace.Interfaces;
using System;
using System.IO;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public sealed class Socket
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IEncoder encoder;
        private TcpClient client;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Socket(TcpClient client, IEncoder encoder)
        {
            this.client = client;
            this.encoder = encoder;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public MessageBase Receive<T>() where T : MessageBase
        {
            try
            {
                StreamReader sr = new StreamReader(this.client.GetStream());
                string msg = sr.ReadLine();
                return this.encoder.Decode<T>(msg);
            }
            catch (Exception)
            {
                this.client.Close();
            }
            return default(T);
        }
        public void Send(MessageBase message)
        {
            try
            {
                string msg = this.encoder.Encode(message);
                StreamWriter sw = new StreamWriter(this.client.GetStream());
                sw.WriteLine(msg);
                sw.Flush();
            }
            catch (Exception e)
            {
                this.client.Close();
            }
        }
        public void Close()
        {
            if (this.client.Connected)
            {
                this.client.Close();
            }
        }

        #endregion
    }
}
