using dotSpace.Interfaces;

namespace dotSpace.BaseClasses
{
    public abstract class ProtocolBase : IProtocol
    {
        public abstract MessageBase Receive(IEncoder encoder);
        public abstract void Send(MessageBase message, IEncoder encoder);
        public abstract void Close();
    }
}
