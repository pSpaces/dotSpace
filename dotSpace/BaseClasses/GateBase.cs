using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotSpace.BaseClasses
{
    public abstract class GateBase : IGate
    {
        public abstract void Start(Action<ISocket, ConnectionMode> callback);
    }
}
