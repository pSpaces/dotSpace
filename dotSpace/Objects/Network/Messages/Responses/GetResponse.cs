using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Responses
{
    [DataContract]
    [KnownType(typeof(BasicResponse))]
    public class GetResponse : BasicResponse
    {
        public GetResponse(string source, string session, string target, IFields result, int code, string message) : base(ActionType.GET_RESPONSE, source, session, target, code, message)
        {
            this.Result = result == null ? null : result.Fields;
        }

        [DataMember]
        public object[] Result { get; set; }
    }
}
