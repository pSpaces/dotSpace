using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a QueryP response.
    /// </summary>
    public sealed class QueryPResponse : ReturnResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryPResponse class.
        /// </summary>
        public QueryPResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryPResponse class.
        /// </summary>
        public QueryPResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.QUERYP_RESPONSE, source, session, target, result, code, message)
        {
        }

        #endregion
    }
}
