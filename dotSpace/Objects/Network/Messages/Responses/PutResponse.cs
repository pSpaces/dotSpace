using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a Put response.
    /// </summary>
    public sealed class PutResponse : ResponseBase
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

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Boxes the message contents from native .NET primitive types into language independent textual representations. 
        /// </summary>
        public override void Box()
        {
        }

        /// <summary>
        /// Unboxes the message contents from language independent textual representations into native .NET primitive types. 
        /// </summary>
        public override void Unbox()
        {
        }

        #endregion
    }
}
