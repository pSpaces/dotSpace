using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a QueryAll request.
    /// </summary>
    public sealed class QueryAllRequest : BasicRequest, IReadRequest
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
        public QueryAllRequest( string source, string session, string target, object[] template) : base( ActionType.QUERYALL_REQUEST, source, session, target)
        {
            this.Template = template;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the template pattern.
        /// </summary>
        public object[] Template { get; set; }

        #endregion
    }
}
