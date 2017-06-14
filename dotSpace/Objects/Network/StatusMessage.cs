namespace dotSpace.Objects.Network
{
    /// <summary>
    /// This entity constitutes the supported status messages used to textually describe the state of the responses.
    /// Each message correspond to the status codes.
    /// </summary>
    internal static class StatusMessage
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        /// <summary>
        /// Request was performed successfully.
        /// </summary>
        public static string OK = "Request was performed successfully.";

        /// <summary>
        /// Failed to receive request or received unknown message.
        /// </summary>
        public static string BAD_REQUEST = "Failed to receive request or received unknown message.";

        /// <summary>
        /// Failed to receive response or received unknown message.
        /// </summary>
        public static string BAD_RESPONSE = "Failed to receive response or received unknown message.";

        /// <summary>
        /// The requested target was unknown or not available.
        /// </summary>
        public static string NOT_FOUND = "The requested target was unknown or not available.";

        /// <summary>
        /// The requested operation was not allowed.
        /// </summary>
        public static string METHOD_NOT_ALLOWED = "The requested operation was not allowed.";

        /// <summary>
        /// The requested connectionmode is not implemented.
        /// </summary>
        public static string NOT_IMPLEMENTED = "The requested connectionmode is not implemented."; 

        #endregion
    }
}
