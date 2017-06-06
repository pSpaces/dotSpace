using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public sealed class PutRequest : BasicRequest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PutRequest(ConnectionMode mode, string source, string session, string target, object[] tuple) : base(mode, ActionType.PUT_REQUEST, source, session, target)
        {
            this.Tuple = tuple;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        [DataMember]
        public object[] Tuple { get; set; } 

        #endregion
    }
}
