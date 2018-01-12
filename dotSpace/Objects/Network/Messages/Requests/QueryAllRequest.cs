using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a QueryAll request.
    /// </summary>
    public sealed class QueryAllRequest : TemplateRequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryAllRequest class.
        /// </summary>
        public QueryAllRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryAllRequest class.
        /// </summary>
        public QueryAllRequest( string source, string session, string target, object[] template) : base( ActionType.QUERYALL_REQUEST, source, session, target, template)
        {
        }

        #endregion
    }
}
