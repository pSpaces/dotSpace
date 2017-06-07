using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public sealed class QueryAllRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public QueryAllRequest()
        {
        }

        public QueryAllRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.QUERYALL_REQUEST, source, session, target)
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
