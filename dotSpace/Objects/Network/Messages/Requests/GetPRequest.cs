using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public sealed class GetPRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructorss

        public GetPRequest()
        {
        }

        public GetPRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.GETP_REQUEST, source, session, target)
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
