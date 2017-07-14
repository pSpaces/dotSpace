using dotSpace.Enumerations;

namespace dotSpace.Interfaces.Network
{
    /// <summary>
    /// Defines the minimal properties any message contain.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets or sets the identify of the original requester.
        /// </summary>
        string Source { get; set; }
        /// <summary>
        /// Gets or sets the unique session identifier used by the source to distinguish requests.
        /// </summary>
        string Session { get; set; }
        /// <summary>
        /// Gets or sets the global identifier that identifies the target space.
        /// </summary>
        string Target { get; set; }
        /// <summary>
        ///  Gets or sets the action to be executed by the remote space.
        /// </summary>
        ActionType Actiontype { get; set; }

        /// <summary>
        /// Boxes the message contents from native .NET primitive types into language independent textual representations. 
        /// </summary>
        void Box();
        /// <summary>
        /// Unboxes the message contents from language independent textual representations into native .NET primitive types. 
        /// </summary>
        void Unbox();
    }
}
