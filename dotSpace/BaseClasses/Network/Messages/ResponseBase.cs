using dotSpace.Enumerations;

namespace dotSpace.BaseClasses.Network.Messages
{
    /// <summary>
    /// Abstract entity containing the minimal properties of any response type message.
    /// </summary>
    public abstract class ResponseBase : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the BasicResponse class.
        /// </summary>
        public ResponseBase()
        {
        }

        /// <summary>
        /// Initializes a new instances of the BasicResponse class.
        /// </summary>
        public ResponseBase(ActionType action, string source, string session, string target, StatusCode code, string message) : base(action, source, session, target)
        {
            this.Code = code;
            this.Message = message;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the status code representing the reponse.
        /// </summary>
        public StatusCode Code { get; set; }

        /// <summary>
        /// Gets or sets the status message as a textual representation of the status code.
        /// </summary>
        public string Message { get; set; }

        #endregion

    }
}
