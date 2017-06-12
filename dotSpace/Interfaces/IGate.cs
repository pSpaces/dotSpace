using System;

namespace dotSpace.Interfaces
{
    public interface IGate
    {
        void Start(Action<IConnectionMode> callback);
        void Stop();
    }
}
