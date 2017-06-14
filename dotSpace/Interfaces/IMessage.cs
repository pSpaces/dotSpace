﻿using dotSpace.Enumerations;

namespace dotSpace.Interfaces
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
    }
}
