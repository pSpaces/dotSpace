using System;

namespace dotSpace.Enumerations
{
    [Flags]
    public enum ConnectionMode
    {
        NONE = 1,
        CONN = 2,
        PUSH = 4,
        PULL = 8,
        KEEP = 16
    }
}
