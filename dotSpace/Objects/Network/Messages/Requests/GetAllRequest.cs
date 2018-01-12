using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a GetAll request.
    /// </summary>
    public sealed class GetAllRequest : TemplateRequestBase
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
        public GetAllRequest(string source, string session, string target, object[] template) : base(ActionType.GETALL_REQUEST, source, session, target, template)
        {
        }

        #endregion
    }
}
