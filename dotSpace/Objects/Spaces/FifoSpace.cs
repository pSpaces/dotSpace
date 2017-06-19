using dotSpace.BaseClasses;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Spaces
{
    /// <summary>
    /// Concrete implementation of a tuplespace datastructure.
    /// Represents a strongly typed set of tuples that can be access through pattern matching. Provides methods to query and manipulate the set.
    /// This class imposes fifo ordering on the underlying tuples.
    /// </summary>
    public sealed class FifoSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the FifoSpace class.
        /// </summary>
        public FifoSpace(ITupleFactory tuplefactory = null) : base(tuplefactory ?? new TupleFactory())
        {
        } 

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Returns the last index contained within the space to force fifo ordering.
        /// </summary>
        protected override int GetIndex(int size)
        {
            return size;
        }

        #endregion
    }
}
