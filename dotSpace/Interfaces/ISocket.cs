using dotSpace.Objects.Network;

namespace dotSpace.Interfaces
{
    public interface ISocket
    {
        MessageBase Receive(IEncoder encoder);
        void Send(MessageBase message, IEncoder encoder);

        void Close();
    }
}
