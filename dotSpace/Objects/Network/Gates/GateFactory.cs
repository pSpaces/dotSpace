using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Gates
{
    public class GateFactory
    {
        public IGate CreateGate(string uri, IEncoder encoder)
        {
            GateInfo gateInfo = new GateInfo(uri);
            IGate gate = null;
            switch (gateInfo.Protocol)
            {
                case Protocol.TCP: gate = new TcpGate(encoder, gateInfo); break;
                case Protocol.UDP: gate = new UdpGate(encoder, gateInfo); break;
                default: break;
            }

            return gate;
        }
    }
}
