using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class QueryResponse : BasicResponse, IResult
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public QueryResponse()
        {
        }

        public QueryResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.QUERY_RESPONSE, source, session, target, code, message)
        {
            this.Result = result != null ? (object[])result.Clone() : null;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public object[] Result { get; set; }

        #endregion
    }
}
