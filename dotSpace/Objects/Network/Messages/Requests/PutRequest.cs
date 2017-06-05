using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public class PutRequest : BasicRequest
    {
        public PutRequest(ConnectionMode mode, string source, string session, string target, object[] tuple) : base(mode, ActionType.PUT_REQUEST, source, session, target)
        {
            this.Tuple = tuple;
        }

        [DataMember]
        public object[] Tuple { get; set; }
    }
}
