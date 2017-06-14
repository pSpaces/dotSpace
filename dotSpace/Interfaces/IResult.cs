namespace dotSpace.Interfaces
{
    /// <summary>
    /// Defines a generalized resultset.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Gets or sets the underlying array of values constituting the tuple values.
        /// </summary>
        object[] Result { get; set; }
    }
}
