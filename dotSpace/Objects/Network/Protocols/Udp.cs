using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace dotSpace.Objects.Network.Protocols
{
    public sealed class Udp : ProtocolBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private UdpClient client;
        private IPEndPoint endpoint;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Udp(UdpClient client, string host, int port)
        {
            this.client = client;            
            //if(connect)
            //this.client.Connect(host, port);
            //else
            //    this.client.Client.Bind()
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override MessageBase Receive(IEncoder encoder)
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
        public override void Send(MessageBase message, IEncoder encoder)
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
        public override void Close()
        {

            this.client.Close();
        }

        #endregion
    }
}
