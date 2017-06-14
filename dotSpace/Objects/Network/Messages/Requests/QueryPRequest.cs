using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a QueryP request.
    /// </summary>
    public sealed class QueryPRequest : BasicRequest, IReadRequest
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
        public QueryPRequest( string source, string session, string target, object[] template) : base( ActionType.QUERYP_REQUEST, source, session, target)
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
