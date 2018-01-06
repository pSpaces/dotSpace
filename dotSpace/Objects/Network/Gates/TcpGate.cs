using dotSpace.BaseClasses.Network;
using dotSpace.Interfaces.Network;
using dotSpace.Objects.Network.Protocols;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace dotSpace.Objects.Network.Gates
{
    /// <summary>
    /// Provides mechanisms for listening of incoming TCP connections, and delegates the connection back through a callback function.
    /// </summary>
    internal sealed class TcpGate : GateBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IPAddress ipAddress;
        private TcpListener listener;
        private bool listening;
        private Action<IConnectionMode> callBack;
        private readonly int backlog;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the TcpGate class.
        /// </summary>
        public TcpGate(IEncoder encoder, ConnectionString connectionstring) : base(encoder, connectionstring)
        {
            this.backlog = 50;
            this.ipAddress = IPAddress.Parse(connectionstring.Host);
            this.listener = new TcpListener(ipAddress, this.ConnectionString.Port);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Starts the process of listening concurrently on the specified host and port.
        /// </summary>
        public override void Start(Action<IConnectionMode> callback)
        {
            if (!this.listening)
            {
                this.callBack = callback;
                this.listening = true;
                new Thread(this.Listen).Start();
            }
        }

        /// <summary>
        /// Stops the listening process.
        /// </summary>
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
            this.listener.Start(this.backlog);
            Console.WriteLine("Current endpoint: {0}:{1}", this.ipAddress.ToString(), this.ConnectionString.Port);
            Console.WriteLine("Begin listening...");
            try
            {
                while (this.listening)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Tcp protocol = new Tcp(client);
                    IConnectionMode mode = this.GetMode(this.ConnectionString.Mode, protocol);
                    new Thread(() => { this.callBack(mode); }).Start();
                }
            }
            catch (Exception e)
            {
                // TODO: Error handling: throw e;
            }
            finally
            {
                listener.Stop();
            }
        }

        #endregion
    }
}
