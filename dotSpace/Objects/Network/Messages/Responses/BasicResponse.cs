using dotSpace.BaseClasses;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public class BasicResponse : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public BasicResponse()
        {
        }

        public BasicResponse(ActionType action, string source, string session, string target, StatusCode code, string message) : base(action, source, session, target)
        {
            this.Code = code;
            this.Message = message;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public StatusCode Code { get; set; }
        public string Message { get; set; }

        #endregion

    }
}
