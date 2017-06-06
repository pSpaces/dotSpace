using dotSpace.Enumerations;
using System;
using System.Runtime.Serialization;

namespace dotSpace.Objects.Network
{
    [DataContract]
    public abstract class MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public MessageBase(ActionType action, string source, string session, string target)
        {
            this.Action = action;
            this.Source = source;
            this.Session = session;
            this.Target = target;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Session { get; set; }
        [DataMember]
        public string Target { get; set; }

        public ActionType Action { get; set; }

        [DataMember(Name = "Action")]
        string ActionString
        {
            get { return this.Action.ToString(); }
            set
            {
                ActionType action;
                this.Action = Enum.TryParse(value, true, out action) ? action : ActionType.NONE;
            }
        } 

        #endregion
    }
}
