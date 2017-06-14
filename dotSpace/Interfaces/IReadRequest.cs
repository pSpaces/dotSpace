namespace dotSpace.Interfaces
{
    /// <summary>
    /// Defines a generalized set for specifying template patterns values.
    /// </summary>
    public interface IReadRequest
    {
        /// <summary>
        /// Gets or sets the underlying array of values constituting the template pattern.
        /// </summary>
        object[] Template { get; set; }
    }
}
