using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using System;
using System.Web.Script.Serialization;

namespace dotSpace.BaseClasses.Network.Messages
{
    /// <summary>
    /// Toplevel entity containing the minimal properties any message contain. This is an abstract class.
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the MessageBase class.
        /// </summary>
        public MessageBase()
        {
        }

        /// <summary>
        /// Initializes a new instances of the MessageBase class.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the identify of the original requester.
        /// </summary>
        private string _source;
        public string Source { get { return _source; } set { _source = value; } }
        /// <summary>
        /// Gets or sets the unique session identifier used by the source to distinguish requests.
        /// </summary>
        public string _session;
        public string Session { get { return _session; } set { _session = value; } }
        /// <summary>
        /// Gets or sets the global identifier that identifies the target space.
        /// </summary>
        public string _target;
        public string Target { get { return _target; } set { _target = value; } }
        /// <summary>
        ///  Gets or sets the action to be executed by the remote space.
        /// </summary>
        [ScriptIgnore]
        public ActionType _actiontype;
        public ActionType Actiontype { get { return _actiontype; } set { _actiontype = value; } }
        /// <summary>
        /// See Actiontype. Specified due to json.
        /// </summary>
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
