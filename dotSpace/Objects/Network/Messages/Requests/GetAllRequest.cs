using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public class GetAllRequest : BasicRequest
    {
        public GetAllRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.GETALL_REQUEST, source, session, target)
        {
            this.Template = template;
        }

        [DataMember]
        public object[] Template { get; set; }
    }
}
