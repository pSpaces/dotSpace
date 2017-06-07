using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public sealed class GetAllRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public GetAllRequest()
        {
        }

        public GetAllRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.GETALL_REQUEST, source, session, target)
        {
            this.Template = template;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public object[] Template { get; set; }

        #endregion
    }
}
