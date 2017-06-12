using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Protocols;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace dotSpace.Objects.Network.Gates
{
    public sealed class TcpGate : GateBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IPAddress ipAddress;
        private TcpListener listener;
        private bool listening;
        private Action<IConnectionMode> callBack;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public TcpGate(IEncoder encoder, GateInfo gateInfo) : base(encoder, gateInfo)
        {
            this.ipAddress = IPAddress.Parse(gateInfo.Host);
            this.listener = new TcpListener(ipAddress, this.gateInfo.Port);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void Start(Action<IConnectionMode> callback)
        {
            if (!this.listening)
            {
                this.callBack = callback;
                this.listening = true;
                new Thread(this.Listen).Start();
            }
        }

        public override void Stop()
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
            Console.WriteLine("Current endpoint: {0}:{1}", this.ipAddress.ToString(), this.gateInfo.Port);
            Console.WriteLine("Begin listening...");
            try
            {
                while (this.listening)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Tcp protocol = new Tcp(client);
                    IConnectionMode mode = this.GetMode(this.gateInfo.Mode, protocol);
                    new Thread(() => { this.callBack(mode); }).Start();
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
