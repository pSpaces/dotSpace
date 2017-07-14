namespace dotSpace.Interfaces.Space
{
    /// <summary>
    /// Provides the primitives required to define a tuple.
    /// </summary>
    public interface ITuple : IFields
    {
        /// <summary>
        /// Returns the size of the tuple.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Gets or sets the i'th element of the tuple.
        /// </summary>
        object this[int idx] { get; set; }

    }
}
