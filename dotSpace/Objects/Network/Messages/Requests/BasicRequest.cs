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
    }
}
