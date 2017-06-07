using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class PutResponse : BasicResponse
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PutResponse()
        {
        }

        public PutResponse(string source, string session, string target, StatusCode code, string message) : base(ActionType.PUT_RESPONSE, source, session, target, code, message)
        {
        }

        #endregion
    }
}
