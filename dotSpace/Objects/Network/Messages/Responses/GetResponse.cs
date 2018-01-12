using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a Get response.
    /// </summary>
    public sealed class GetResponse : ReturnResponseBase
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
        public GetResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GET_RESPONSE, source, session, target, result, code, message)
        {
            this.Result = result;
        }

        #endregion
    }
}
