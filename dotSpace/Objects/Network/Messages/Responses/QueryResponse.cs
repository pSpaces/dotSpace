using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a Query response.
    /// </summary>
    public sealed class QueryResponse : ReturnResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryResponse class.
        /// </summary>
        public QueryResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryResponse class.
        /// </summary>
        public QueryResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.QUERY_RESPONSE, source, session, target, result, code, message)
        {
        }

        #endregion
    }
}
