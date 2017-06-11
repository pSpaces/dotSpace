using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace dotSpace.Objects.Network
{
    public sealed class TcpGate : GateBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private readonly int port;
        private IPAddress ipAddress;
        private TcpListener listener;
        private bool listening;
        private Action<ISocket, ConnectionMode> callBack;
        private ConnectionMode mode;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public TcpGate(GateInfo gateInfo)
        {
            this.port = gateInfo.Port;
            this.ipAddress = IPAddress.Parse(gateInfo.Host);
            this.mode = gateInfo.Mode;
            this.listener = new System.Net.Sockets.TcpListener(ipAddress, this.port);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void Start(Action<ISocket, ConnectionMode> callback)
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

        private void Listen()
        {
            this.listener.Start(121);
            Console.WriteLine("Current endpoint: {0}:{1}", this.ipAddress.ToString(), this.port);
            Console.WriteLine("Begin listening...");
            try
            {
                while (this.listening)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    TcpSocket socket = new TcpSocket(client);
                    new Thread(() => { this.callBack(socket, this.mode); }).Start();
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
