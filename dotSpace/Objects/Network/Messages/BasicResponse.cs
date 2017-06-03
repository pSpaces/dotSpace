using dotSpace.Enumerations;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network
{
    [DataContract]
    [KnownType(typeof(MessageBase))]
    public class BasicResponse : MessageBase
    {
        public BasicResponse(ActionType action, string source, string session, string target, int code, string message) : base(action, source, session, target)
        {
            this.Code = code;
            this.Message = message;
        }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Message { get; set; }

    }
}
