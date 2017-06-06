using dotSpace.Enumerations;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Responses
{
    [DataContract]
    [KnownType(typeof(BasicResponse))]
    public class GetAllResponse : BasicResponse
    {
        public GetAllResponse(string source, string session, string target, IEnumerable<object[]> result, StatusCode code, string message) : base(ActionType.GETALL_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        [DataMember]
        public IEnumerable<object[]> Result { get; set; }
    }
}
