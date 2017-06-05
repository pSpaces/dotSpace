using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public class QueryPRequest : BasicRequest
    {
        public QueryPRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.QUERYP_REQUEST, source, session, target)
        {
            this.Template = template;
        }

        [DataMember]
        public object[] Template { get; set; }
    }
}
