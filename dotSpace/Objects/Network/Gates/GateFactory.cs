using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Gates
{
    public class GateFactory
    {
        public IGate CreateGate(string uri, IEncoder encoder)
        {
            ConnectionString connectionString = new ConnectionString(uri);
            IGate gate = null;
            switch (connectionString.Protocol)
            {
                case Protocol.TCP: gate = new TcpGate(encoder, connectionString); break;
                case Protocol.UDP: gate = new UdpGate(encoder, connectionString); break;
                default: break;
            }

            return gate;
        }
    }
}
