using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a Get response.
    /// </summary>
    public sealed class GetResponse : BasicResponse, IResult
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GetResponse class.
        /// </summary>
        public GetResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetResponse class.
        /// </summary>
        public GetResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GET_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values.
        /// </summary>
        public object[] Result { get; set; }

        #endregion
    }
}
