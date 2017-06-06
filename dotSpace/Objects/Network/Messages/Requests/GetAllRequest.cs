using dotSpace.Enumerations;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public sealed class GetAllRequest : BasicRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public GetAllRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.GETALL_REQUEST, source, session, target)
        {
            this.Template = template;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        [DataMember]
        public object[] Template { get; set; } 

        #endregion
    }
}
