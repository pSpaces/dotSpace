using dotSpace.Enumerations;

namespace dotSpace.BaseClasses.Network.Messages
{
    /// <summary>
    /// Abstract entity containing the minimal properties of any request type message.
    /// </summary>
    public abstract class RequestBase : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the BasicRequest class.
        /// </summary>
        public RequestBase()
        {
        }

        /// <summary>
        /// Initializes a new instances of the BasicRequest class.
        /// </summary>
        public RequestBase(ActionType action, string source, string session, string target) : base(action, source, session, target)
        {
        }

        #endregion
    }
}
