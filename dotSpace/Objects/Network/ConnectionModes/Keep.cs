using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace dotSpace.Objects.Network.Protocols
{
    public sealed class Keep : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private MessageQueue messageQueue;
        private Thread receiveThread; 

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Keep(ISocket socket, IEncoder encoder) : base(socket, encoder)
        {
            this.messageQueue = new MessageQueue();
            this.receiveThread = new Thread(this.Receive);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public override void ProcessRequest(OperationMap operationMap)
        {
            while (true) // FIX THIS
            {
                BasicRequest request = (BasicRequest)this.socket.Receive(this.encoder);
                var t = Task.Factory.StartNew(() =>
                {
                    BasicRequest req = request;
                    req = this.ValidateRequest(req);
                    BasicResponse response = operationMap.Execute(req);
                    lock (this.socket)
                    {
                        this.socket.Send(response, this.encoder);
                    }
                }
                );
            }
        }

        public override T PerformRequest<T>(BasicRequest request)
        {
            lock (this.socket)
            {
                if (!this.receiveThread.IsAlive)
                    this.receiveThread.Start();
                this.socket.Send(request, this.encoder);
            }
            return (T)this.messageQueue.Get(request.Session);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private void Receive()
        {
            while (true) // FIX THIS
            {
                MessageBase message = this.socket.Receive(this.encoder);
                message = this.ValidateResponse(message);
                this.messageQueue.Put(message);
            }
        } 

        #endregion
    }
}
