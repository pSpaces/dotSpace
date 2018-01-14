using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a GetP response.
    /// </summary>
    public sealed class GetPResponse : ReturnResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GetPResponse class.
        /// </summary>
        public GetPResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetPResponse class.
        /// </summary>
        public GetPResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GETP_RESPONSE, source, session, target, result, code, message)
        {
        }

        #endregion
    }
}
