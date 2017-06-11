using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public sealed class PutRequest : BasicRequest, IWriteRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PutRequest()
        {
        }

        public PutRequest(string source, string session, string target, object[] tuple) : base( ActionType.PUT_REQUEST, source, session, target)
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
