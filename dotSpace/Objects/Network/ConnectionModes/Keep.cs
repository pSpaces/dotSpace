using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using dotSpace.Objects.Utility;
using System.Threading;
using System.Threading.Tasks;

namespace dotSpace.Objects.Network.ConnectionModes
{
    /// <summary>
    /// Implements the mechanisms to support the KEEP connection scheme.
    /// </summary>
    public sealed class Keep : ConnectionModeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private MessageQueue messageQueue;
        private Thread receiveThread;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the Keep class.
        /// </summary>
        public Keep(IProtocol protocol, IEncoder encoder) : base(protocol, encoder)
        {
            this.messageQueue = new MessageQueue();
            this.receiveThread = new Thread(this.Receive);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods
        
        /// <summary>
        /// Waits for an incoming message, then executes the corresponding operation concurrently.
        /// Once the operation completes, it transmits a response back.
        /// </summary>
        public override void ProcessRequest(IOperationMap operationMap)
        {
            while (true) // FIX THIS
            {
                BasicRequest request = (BasicRequest)this.protocol.Receive(this.encoder);
                var t = Task.Factory.StartNew(() =>
                {
                    BasicRequest req = request;
                    req = (BasicRequest)this.ValidateRequest(req);
                    BasicResponse response = (BasicResponse)operationMap.Execute(req);
                    lock (this.protocol)
                    {
                        this.protocol.Send(response, this.encoder);
                    }
                }
                );
            }
        }
        /// <summary>
        /// Sends a request, and waits for a response matching the session. This is a blocking operation.
        /// </summary>
        public override T PerformRequest<T>(IMessage request)
        {
            lock (this.protocol)
            {
                if (!this.receiveThread.IsAlive)
                    this.receiveThread.Start();
                this.protocol.Send(request, this.encoder);
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
                MessageBase message = (MessageBase)this.protocol.Receive(this.encoder);
                message = (MessageBase)this.ValidateResponse(message);
                this.messageQueue.Put(message);
            }
        }

        #endregion
    }
}
