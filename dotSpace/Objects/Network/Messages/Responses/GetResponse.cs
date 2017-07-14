using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;
using dotSpace.Objects.Json;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a Get response.
    /// </summary>
    public sealed class GetResponse : ResponseBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the GetResponse class.
        /// </summary>
        public GetResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the GetResponse class.
        /// </summary>
        public GetResponse(string source, string session, string target, object[] result, StatusCode code, string message) : base(ActionType.GET_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values.
        /// </summary>
        public object[] Result { get; set; }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Boxes the message contents from native .NET primitive types into language independent textual representations. 
        /// </summary>
        public override void Box()
        {
            this.Result = TypeConverter.Box(this.Result);
        }

        /// <summary>
        /// Unboxes the message contents from language independent textual representations into native .NET primitive types. 
        /// </summary>
        public override void Unbox()
        {
            this.Result = TypeConverter.Unbox(this.Result);
        }

        #endregion
    }
}
