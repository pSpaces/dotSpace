namespace dotSpace.Enumerations
{
    /// <summary>
    /// This entity constitutes the supported protocols of the distributed tuple space.
    /// </summary>
    public enum Protocol
    {
        NONE,
        /// <summary>
        /// Communication between a spacerepository and a remotespace is done utilizing TCP.
        /// </summary>
        TCP,
        /// <summary>
        /// Communication between a spacerepository and a remotespace is done utilizing UDP.
        /// This protocol has not been fully implemented, and as such only the basic entities
        /// are provided. 
        /// </summary>
        UDP,
    }
}
