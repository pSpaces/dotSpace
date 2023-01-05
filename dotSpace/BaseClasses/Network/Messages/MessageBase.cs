using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using System;

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
        public string Source { get; set; }
        /// <summary>
        /// Gets or sets the unique session identifier used by the source to distinguish requests.
        /// </summary>
        public string Session { get; set; }
        /// <summary>
        /// Gets or sets the global identifier that identifies the target space.
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        ///  Gets or sets the action to be executed by the remote space.
        /// </summary>
        public ActionType Actiontype { get; set; }
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

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Boxes the message contents from native .NET primitive types into language independent textual representations. 
        /// </summary>
        public abstract void Box();
        /// <summary>
        /// Unboxes the message contents from language independent textual representations into native .NET primitive types. 
        /// </summary>
        public abstract void Unbox();

        #endregion
    }
}
