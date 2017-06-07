using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class QueryPResponse : BasicResponse
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public QueryPResponse()
        {
        }

        public QueryPResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.QUERYP_RESPONSE, source, session, target, code, message)
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
