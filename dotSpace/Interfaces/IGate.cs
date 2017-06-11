using dotSpace.Enumerations;
using dotSpace.Objects.Network;
using System;

namespace dotSpace.Interfaces
{
    public interface IGate
    {
        void Start(Action<ISocket, ConnectionMode> callback);
    }
}
