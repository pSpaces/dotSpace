using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a Put request.
    /// </summary>
    public sealed class PutRequest : RequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the PutRequest class.
        /// </summary>
        public PutRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the PutRequest class.
        /// </summary>
        public PutRequest(string source, string session, string target, object[] tuple) : base( ActionType.PUT_REQUEST, source, session, target)
        {
            this.Tuple = tuple;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values.
        /// </summary>
        public object[] Tuple { get; set; }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Boxes the message contents from native .NET primitive types into language independent textual representations. 
        /// </summary>
        public override void Box()
        {
            this.Tuple = TypeConverter.Box(this.Tuple);
        }

        /// <summary>
        /// Unboxes the message contents from language independent textual representations into native .NET primitive types. 
        /// </summary>
        public override void Unbox()
        {
            this.Tuple = TypeConverter.Unbox(this.Tuple);
        }

        #endregion
    }
}
