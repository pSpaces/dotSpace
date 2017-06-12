using dotSpace.Enumerations;
using System;
using System.Web.Script.Serialization;

namespace dotSpace.BaseClasses
{
    public abstract class MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors
        public MessageBase()
        {
        }

        public MessageBase(ActionType action, string source, string session, string target)
        {
            this.Actiontype = action;
            this.Source = source;
            this.Session = session;
            this.Target = target;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public string Source { get; set; }
        public string Session { get; set; }
        public string Target { get; set; }

        [ScriptIgnore]
        public ActionType Actiontype { get; set; }

        public string Action
        {
            get { return this.Actiontype.ToString(); }
            set
            {
                ActionType action;
                this.Actiontype = Enum.TryParse(value, true, out action) ? action : ActionType.NONE;
            }
        }

        #endregion
    }
}
