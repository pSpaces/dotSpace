using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a GetAll request.
    /// </summary>
    public sealed class GetAllRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GetAllRequest class.
        /// </summary>
        public GetAllRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetAllRequest class.
        /// </summary>
        public GetAllRequest(string source, string session, string target, object[] template) : base(ActionType.GETALL_REQUEST, source, session, target)
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
