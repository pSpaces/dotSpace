using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a QueryP request.
    /// </summary>
    public sealed class QueryPRequest : TemplateRequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryPRequest class.
        /// </summary>
        public QueryPRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryPRequest class.
        /// </summary>
        public QueryPRequest(string source, string session, string target, object[] template) : base(ActionType.QUERYP_REQUEST, source, session, target, template)
        {
        }

        #endregion
    }
}
