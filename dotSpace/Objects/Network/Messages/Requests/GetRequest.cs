using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a Get request.
    /// </summary>
    public sealed class GetRequest : TemplateRequestBase
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
        public GetRequest(string source, string session, string target, object[] template) : base(ActionType.GET_REQUEST, source, session, target, template)
        {
        }

        #endregion
    }
}
