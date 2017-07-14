namespace dotSpace.Interfaces.Space
{
    /// <summary>
    /// Provides the primitives required to define a pattern.
    /// </summary>
    public interface IPattern : IFields
    {
        /// <summary>
        /// Returns the size of the pattern.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets or sets the i'th element of the pattern.
        /// </summary>
        object this[int idx] { get; }
    }
}
