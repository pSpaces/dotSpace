namespace dotSpace.Interfaces.Space
{
    /// <summary>
    /// Defines the operations neccessary to create custom tuples used by the concrete spaces.
    /// </summary>
    public interface ITupleFactory
    {
        /// <summary>
        /// Returns a new instance of a tuple.
        /// </summary>
        ITuple Create(params object[] fields);
    }
}
