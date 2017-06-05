using dotSpace.Enumerations;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Responses
{
    [DataContract]
    [KnownType(typeof(BasicResponse))]
    public class PutResponse : BasicResponse
    {
        public PutResponse(string source, string session, string target, int code, string message) : base(ActionType.PUT_RESPONSE, source, session, target, code, message)
        {
        }
    }
}
