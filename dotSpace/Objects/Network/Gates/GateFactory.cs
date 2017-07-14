using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;

namespace dotSpace.Objects.Network.Gates
{
    /// <summary>
    /// This class constitutes a factory pattern for instantiation of gates.
    /// </summary>
    public class GateFactory
    {
        /// <summary>
        /// Returns a new instance of a gate based on the provided connectionstring.
        /// </summary>
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
