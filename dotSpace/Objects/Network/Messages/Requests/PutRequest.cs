using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public sealed class PutRequest : BasicRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PutRequest()
        {
        }

        public PutRequest(ConnectionMode mode, string source, string session, string target, object[] tuple) : base(mode, ActionType.PUT_REQUEST, source, session, target)
        {
            this.Tuple = tuple;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public object[] Tuple { get; set; }

        #endregion
    }
}
