using System;
using System.IO;
using System.Net.Sockets;

namespace dotSpace.Objects.Network
{
    public abstract class SocketBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected TcpClient client;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public SocketBase(TcpClient client)
        {
            this.client = client;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public MessageBase Receive<T>() where T : MessageBase
        {
            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                //string msg = (string)formatter.Deserialize(client.GetStream());
                StreamReader sr = new StreamReader(this.client.GetStream());
                string msg = sr.ReadLine();
                return this.Decode<T>(msg);
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
                string msg = this.Encode(message);
                StreamWriter sw = new StreamWriter(this.client.GetStream());
                sw.WriteLine(msg);
                sw.Flush();
                //BinaryFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(client.GetStream(), msg);
            }
            catch (Exception)
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

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected abstract MessageBase Decode<T>(string msg) where T : MessageBase;
        protected string Encode(MessageBase message)
        {
            return message.Serialize();
        } 

        #endregion
    }
}
