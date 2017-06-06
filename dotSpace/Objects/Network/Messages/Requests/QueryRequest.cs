using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public sealed class QueryRequest : BasicRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public QueryRequest(ConnectionMode mode, string source, string session, string target, object[] template) : base(mode, ActionType.QUERY_REQUEST, source, session, target)
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
