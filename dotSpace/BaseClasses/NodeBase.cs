using dotSpace.Interfaces;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace dotSpace.BaseClasses
{
    public abstract class NodeBase
    {
        public abstract ITuple Get(string target, IPattern pattern);
        public abstract ITuple GetP(string target, IPattern pattern);
        public abstract ITuple Query(string target, IPattern pattern);
        public abstract ITuple QueryP(string target, IPattern pattern);
        public abstract void Put(string target, ITuple t);

        protected IPEndPoint CreateEndpoint(string host, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(host);
            return new IPEndPoint(ipAddress, port);
        }

        protected string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
