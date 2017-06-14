using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a Get request.
    /// </summary>
    public sealed class GetRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GetRequest class.
        /// </summary>
        public GetRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetRequest class.
        /// </summary>
        public GetRequest(string source, string session, string target, object[] template) : base(ActionType.GET_REQUEST, source, session, target)
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
