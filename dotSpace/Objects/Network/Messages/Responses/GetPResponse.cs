using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class GetPResponse : BasicResponse
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public GetPResponse()
        {
        }

        public GetPResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GETP_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public object[] Result { get; set; }

        #endregion
    }
}
