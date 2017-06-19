namespace dotSpace.Interfaces
{
    /// <summary>
    /// Defines the operations neccessary to create custom tuples used in the implemented spaces.
    /// </summary>
    public interface ITupleFactory
    {
        /// <summary>
        /// Returns a new instance of a tuple.
        /// </summary>
        ITuple Create(params object[] fields);
    }
}
