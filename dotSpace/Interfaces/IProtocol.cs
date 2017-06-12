using dotSpace.BaseClasses;

namespace dotSpace.Interfaces
{
    public interface IProtocol
    {
        MessageBase Receive(IEncoder encoder);
        void Send(MessageBase message, IEncoder encoder);
        void Close();
    }
}
