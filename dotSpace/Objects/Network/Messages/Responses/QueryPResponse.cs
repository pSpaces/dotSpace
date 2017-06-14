using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a QueryP response.
    /// </summary>
    public sealed class QueryPResponse : BasicResponse, IResult
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
        public QueryPResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.QUERYP_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values.
        /// </summary>
        public object[] Result { get; set; }

        #endregion
    }
}
