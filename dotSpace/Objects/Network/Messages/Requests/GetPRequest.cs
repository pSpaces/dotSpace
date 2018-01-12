using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a GetP request.
    /// </summary>
    public sealed class GetPRequest : TemplateRequestBase
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
        public GetPRequest( string source, string session, string target, object[] template) : base( ActionType.GETP_REQUEST, source, session, target, template)
        {
        }

        #endregion
    }
}
