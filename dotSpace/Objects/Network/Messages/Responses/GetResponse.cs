using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class GetResponse : BasicResponse
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public GetResponse()
        {
        }

        public GetResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GET_RESPONSE, source, session, target, code, message)
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
