using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Concrete entity containing the minimal properties of any response type message.
    /// </summary>
    public class BasicResponse : ResponseBase  
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the BasicResponse class.
        /// </summary>
        public BasicResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the BasicResponse class.
        /// </summary>
        public BasicResponse(ActionType action, string source, string session, string target, StatusCode code, string message) : base(action, source, session, target, code, message)
        {
        }

        #endregion

    }
}
