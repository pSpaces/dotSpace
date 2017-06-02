using dotSpace.Enumerations;
using System;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network
{
    [DataContract]
    [KnownType(typeof(MessageBase))]
    public abstract class RequestBase : MessageBase
    {
        public RequestBase(ConnectionMode mode, ActionType action, string source, string session, string target) : base(action, source, session, target)
        {
            this.Mode = mode;

        }
        public ConnectionMode Mode { get; set; }
        [DataMember(Name = "Mode")]
        string ModeString
        {
            get { return this.Mode.ToString(); }
            set
            {
                ConnectionMode mode;
                this.Mode = Enum.TryParse(value, true, out mode) ? mode : ConnectionMode.NONE;
            }
        }


    }
}
