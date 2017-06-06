using dotSpace.Enumerations;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Responses
{
    [DataContract]
    [KnownType(typeof(MessageBase))]
    public class BasicResponse : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public BasicResponse(ActionType action, string source, string session, string target, StatusCode code, string message) : base(action, source, session, target)
        {
            this.Code = code;
            this.Message = message;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        [DataMember]
        public StatusCode Code { get; set; }
        [DataMember]
        public string Message { get; set; } 

        #endregion

    }
}
