namespace dotSpace.Interfaces
{
    /// <summary>
    /// Defines a generalized set for specifying tuple values.
    /// </summary>
    public interface IWriteRequest
    {
        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values.
        /// </summary>
        object[] Tuple { get; set; }
    }
}
