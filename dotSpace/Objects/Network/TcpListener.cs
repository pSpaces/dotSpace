
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotSpace.Objects.Network
{
    class TcpListener
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private readonly int port;
        private IPAddress ipAddress;
        private System.Net.Sockets.TcpListener listener;
        private bool listening;
        private Action<TcpClient> callBack;
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public TcpListener(int port)
        {
            this.port = port;
            this.ipAddress = IPAddress.Parse(this.GetLocalIPAddress());
            this.listener = new System.Net.Sockets.TcpListener(ipAddress, this.port);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public void Start(Action<TcpClient> callback)
        {
            if (!this.listening)
            {
                this.callBack = callback;
                this.listening = true;
                new Thread(this.Listen).Start();
            }
        }

        public void Stop()
        {
            this.listening = false;
            this.listener.Stop();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private string GetLocalIPAddress()
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

        private void Listen()
        {
            this.listener.Start(11);
            Console.WriteLine("Current endpoint: {0}:{1}", this.ipAddress.ToString(), this.port);
            Console.WriteLine("Begin listening...");
            try
            {
                while (this.listening)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    new Thread((x) => { this.callBack((TcpClient)x); }).Start(client);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                listener.Stop();
            }
        }

        #endregion
    }
}
