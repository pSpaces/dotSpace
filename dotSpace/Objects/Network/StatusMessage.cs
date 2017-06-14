namespace dotSpace.Objects.Network
{
    internal static class StatusMessage
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        public static string OK = "Request was performed successfully.";
        public static string BAD_REQUEST = "Failed to receive request or received unknown message.";
        public static string BAD_RESPONSE = "Failed to receive response or received unknown message.";
        public static string NOT_FOUND = "The requested target was unknown or not available.";
        public static string METHOD_NOT_ALLOWED = "The requested operation was not allowed.";
        public static string NOT_IMPLEMENTED = "The requested connectionmode is not implemented."; 

        #endregion
    }
}
