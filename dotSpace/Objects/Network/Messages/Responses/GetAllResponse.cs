using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a GetAll response.
    /// </summary>
    public sealed class GetAllResponse : ReturnResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GetAllResponse class.
        /// </summary>
        public GetAllResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetAllResponse class.
        /// </summary>
        public GetAllResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GETALL_RESPONSE, source, session, target, result, code, message)
        {
        }

        #endregion
    }
}
