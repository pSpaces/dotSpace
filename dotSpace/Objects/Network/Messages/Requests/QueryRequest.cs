using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public sealed class QueryRequest : BasicRequest, IReadRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public QueryRequest()
        {
        }

        public QueryRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.QUERY_REQUEST, source, session, target)
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
