using dotSpace.Interfaces;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace dotSpace.BaseClasses
{
    public abstract class NodeBase
    {
        public abstract ITuple Get(string identifier, IPattern pattern);
        public abstract ITuple GetP(string identifier, IPattern pattern);
        public abstract ITuple Query(string identifier, IPattern pattern);
        public abstract ITuple QueryP(string identifier, IPattern pattern);
        public abstract void Put(string identifier, ITuple t);

        protected abstract T Decode<T>(string msg) where T : MessageBase;
        protected string Encode(MessageBase message)
        {
            return message.Serialize();
        }
        protected T Receive<T>(TcpClient client) where T : MessageBase
        {
            try
            {
                //BinaryFormatter formatter = new BinaryFormatter();
                //string msg = (string)formatter.Deserialize(client.GetStream());
                using (StreamReader sr = new StreamReader(client.GetStream()))
                {
                    string msg = sr.ReadLine();
                    return this.Decode<T>(msg);
                }
            }
            catch (Exception)
            {
                client.Close();
            }
            return default(T);
        }
        protected void Send(TcpClient client, MessageBase message)
        {
            try
            {
                string msg = this.Encode(message);
                using (StreamWriter sw = new StreamWriter(client.GetStream()))
                {
                    sw.Write(msg);
                    sw.Flush();
                }
                //BinaryFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(client.GetStream(), msg);
            }
            catch (Exception e)
            {
                client.Close();
            }
        }
    }
}
