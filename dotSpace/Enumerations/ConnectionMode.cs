namespace dotSpace.Enumerations
{
    /// <summary>
    /// This entity constitutes the supported connection modes when communicating with the distributed tuple space.
    /// </summary>
    public enum ConnectionMode
    {
        NONE,
        /// <summary>
        /// CONN is a simple request/response pattern where the underlying connection is terminated after each operation.
        /// </summary>
        CONN,
        /// <summary>
        /// PUSH is a request/response pattern where the underlying connection is terminated after the request is sent.
        /// Once the space repository has completed the execution of the operation, it then connects back to the requestee, and 
        /// sends the response.
        /// </summary>
        PUSH,
        /// <summary>
        /// PULL is a request/response pattern using polling. The underlying connection is terminated after the request is sent.
        /// The requestee then repeatedly re-establishes the connection prompting for status on the operation.
        /// Once the space repository has completed the execution of the operation, it then sends the response.
        /// </summary>
        PULL,
        /// <summary>
        /// KEEP is a request/response pattern where the underlying connection is kept alive throughout the entire session.
        /// </summary>
        KEEP
    }
}
