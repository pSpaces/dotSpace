using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a Query request.
    /// </summary>
    public sealed class QueryRequest : BasicRequest, IReadRequest
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
        public QueryRequest(string source, string session, string target, object[] template) : base( ActionType.QUERY_REQUEST, source, session, target)
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
