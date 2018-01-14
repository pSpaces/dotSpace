using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a QueryAll response.
    /// </summary>
    public sealed class QueryAllResponse : ReturnResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryAllResponse class.
        /// </summary>
        public QueryAllResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryAllResponse class.
        /// </summary>
        public QueryAllResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.QUERYALL_RESPONSE, source, session, target, result, code, message)
        {
        }

        #endregion
    }
}
