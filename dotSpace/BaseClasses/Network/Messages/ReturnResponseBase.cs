using dotSpace.Enumerations;

namespace dotSpace.BaseClasses.Network.Messages
{
    /// <summary>
    /// Entity representing a message of a Query response.
    /// </summary>
    public abstract class ReturnResponseBase : ResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryResponse class.
        /// </summary>
        public ReturnResponseBase()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryResponse class.
        /// </summary>
        public ReturnResponseBase(ActionType action, string source, string session, string target, object[] result, StatusCode code, string message) : base(action, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values given as response to a request.
        /// </summary>
        public object[] Result { get; set; }

        #endregion
    }
}
