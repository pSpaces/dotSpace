using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Concrete entity containing the minimal properties of any request type message.
    /// </summary>
    public class BasicRequest : RequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the BasicRequest class.
        /// </summary>
        public BasicRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the BasicRequest class.
        /// </summary>
        public BasicRequest(ActionType action, string source, string session, string target) : base(action, source, session, target)
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
