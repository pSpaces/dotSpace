using dotSpace.Interfaces;
using dotSpace.Objects.Network;

namespace dotSpace.BaseClasses
{
    public abstract class SocketBase : ISocket
    {
        public abstract MessageBase Receive(IEncoder encoder);
        public abstract void Send(MessageBase message, IEncoder encoder);
        public abstract void Close();
    }
}
