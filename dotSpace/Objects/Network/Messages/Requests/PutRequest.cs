using dotSpace.BaseClasses.Network.Messages;
using dotSpace.Enumerations;

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
    }
}
