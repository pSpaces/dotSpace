using dotSpace.Enumerations;
using System;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    [DataContract]
    [KnownType(typeof(MessageBase))]
    public class BasicRequest : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public BasicRequest(ConnectionMode mode, ActionType action, string source, string session, string target) : base(action, source, session, target)
        {
            this.Mode = mode;

        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public ConnectionMode Mode { get; set; }
        [DataMember(Name = "Mode")]
        public string ModeString
        {
            get { return this.Mode.ToString(); }
            set
            {
                ConnectionMode mode;
                this.Mode = Enum.TryParse(value, true, out mode) ? mode : ConnectionMode.NONE;
            }
        } 

        #endregion

    }
}
