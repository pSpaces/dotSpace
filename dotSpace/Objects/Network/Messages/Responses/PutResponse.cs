using dotSpace.Enumerations;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Responses
{
    [DataContract]
    [KnownType(typeof(BasicResponse))]
    public sealed class PutResponse : BasicResponse
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PutResponse(string source, string session, string target, StatusCode code, string message) : base(ActionType.PUT_RESPONSE, source, session, target, code, message)
        {
        } 

        #endregion
    }
}
