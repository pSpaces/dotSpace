namespace dotSpace.Enumerations
{
    /// <summary>
    /// This entity constitutes the supported status codes used to describe the state of the responses.
    /// The status codes are similar to that of typical HTTP responses.
    /// </summary>
    public enum StatusCode : int
    {
        /// <summary>
        /// The operation was completed successfully.
        /// </summary>
        OK = 200,

        /// <summary>
        /// The received message was expected to be the format of a request.
        /// </summary>
        BAD_REQUEST = 400,

        /// <summary>
        /// The received message was expected to be the format of a response.
        /// </summary>
        BAD_RESPONSE = 401,

        /// <summary>
        /// The specified target space was not found in the repository.
        /// </summary>
        NOT_FOUND = 404,

        /// <summary>
        /// Unknown or unsupported operation.
        /// </summary>
        METHOD_NOT_ALLOWED = 405,

        /// <summary>
        /// The requested connectionmode is not supported.
        /// </summary>
        NOT_IMPLEMENTED = 501
    }
}
