using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.ConnectionModes;
using System;

namespace dotSpace.BaseClasses
{
    public abstract class GateBase : IGate
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        protected GateInfo gateInfo;
        protected IEncoder encoder;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public GateBase(IEncoder encoder, GateInfo gateInfo)
        {
            this.encoder = encoder;
            this.gateInfo = gateInfo;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public abstract void Start(Action<IConnectionMode> callback);
        public abstract void Stop();
        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected IConnectionMode GetMode(ConnectionMode connectionmode, IProtocol protocol)
        {
            switch (connectionmode)
            {
                case ConnectionMode.CONN: return new Conn(protocol, this.encoder);
                case ConnectionMode.KEEP: return new Keep(protocol, this.encoder);
                case ConnectionMode.PULL: return null;
                case ConnectionMode.PUSH: return null;
                default: return null;
            }
        } 

        #endregion
    }
}
