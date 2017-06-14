using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a GetP request.
    /// </summary>
    public sealed class GetPRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructorss

        /// <summary>
        /// Initializes a new instances of the GetPRequest class.
        /// </summary>
        public GetPRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetPRequest class.
        /// </summary>
        public GetPRequest( string source, string session, string target, object[] template) : base( ActionType.GETP_REQUEST, source, session, target)
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
