using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a Query request.
    /// </summary>
    public sealed class QueryRequest : TemplateRequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryRequest class.
        /// </summary>
        public QueryRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryRequest class.
        /// </summary>
        public QueryRequest(string source, string session, string target, object[] template) : base( ActionType.QUERY_REQUEST, source, session, target, template)
        {
        }

        #endregion
    }
}
