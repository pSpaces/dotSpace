using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace dotSpace.BaseClasses
{
    public abstract class RepositoryBase : IRepository
    {

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public RepositoryBase()
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods
        public abstract ISpace GetSpace(string target);
        public abstract ITuple Get(string target, IPattern pattern);
        public abstract ITuple Get(string target, params object[] pattern);
        public abstract ITuple GetP(string target, IPattern pattern);
        public abstract ITuple GetP(string target, params object[] pattern);
        public abstract IEnumerable<ITuple> GetAll(string target, IPattern pattern);
        public abstract IEnumerable<ITuple> GetAll(string target, params object[] pattern);
        public abstract ITuple Query(string target, IPattern pattern);
        public abstract ITuple Query(string target, params object[] pattern);
        public abstract ITuple QueryP(string target, IPattern pattern);
        public abstract ITuple QueryP(string target, params object[] pattern);
        public abstract IEnumerable<ITuple> QueryAll(string target, IPattern pattern);
        public abstract IEnumerable<ITuple> QueryAll(string target, params object[] pattern);
        public abstract void Put(string target, ITuple tuple);
        public abstract void Put(string target, params object[] tuple);
        public IPEndPoint CreateEndpoint(string address, int port)
        {
            IPAddress ipAddress = IPAddress.Parse(address);
            return new IPEndPoint(ipAddress, port);
        }
        public string GetLocalIPAddress()
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

        #endregion
    }
}
