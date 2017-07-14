using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Requests
{
    /// <summary>
    /// Entity representing a message of a GetP request.
    /// </summary>
    public sealed class GetPRequest : RequestBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructorss

        /// <summary>
        /// Initializes a new instances of the GetPRequest class.
        /// </summary>
        public GetPRequest()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetPRequest class.
        /// </summary>
        public GetPRequest( string source, string session, string target, object[] template) : base( ActionType.GETP_REQUEST, source, session, target)
        {
            this.Template = template;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the template pattern.
        /// </summary>
        public object[] Template { get; set; }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Boxes the message contents from native .NET primitive types into language independent textual representations. 
        /// </summary>
        public override void Box()
        {
            this.Template = TypeConverter.Box(this.Template);
        }

        /// <summary>
        /// Unboxes the message contents from language independent textual representations into native .NET primitive types. 
        /// </summary>
        public override void Unbox()
        {
            this.Template = TypeConverter.Unbox(this.Template);
        }

        #endregion
    }
}
