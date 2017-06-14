using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a Put response.
    /// </summary>
    public sealed class PutResponse : BasicResponse
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the PutResponse class.
        /// </summary>
        public PutResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the PutResponse class.
        /// </summary>
        public PutResponse(string source, string session, string target, StatusCode code, string message) : base(ActionType.PUT_RESPONSE, source, session, target, code, message)
        {
        }

        #endregion
    }
}
