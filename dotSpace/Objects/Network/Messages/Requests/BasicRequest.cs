using dotSpace.Enumerations;
using System;
using System.Web.Script.Serialization;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public class BasicRequest : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors
        public BasicRequest()
        {
        }

        public BasicRequest(ConnectionMode mode, ActionType action, string source, string session, string target) : base(action, source, session, target)
        {
            this.Connectionmode = mode;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        [ScriptIgnore]
        public ConnectionMode Connectionmode { get; set; }

        public string Mode
        {
            get { return this.Connectionmode.ToString(); }
            set
            {
                ConnectionMode mode;
                this.Connectionmode = Enum.TryParse(value, true, out mode) ? mode : ConnectionMode.NONE;
            }
        }

        #endregion

    }
}
