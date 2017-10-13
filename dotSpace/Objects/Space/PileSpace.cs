using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace dotSpace.Objects.Space
{
    /// <summary>
    /// Concrete implementation of a tuplespace datastructure.
    /// Represents a strongly typed set of tuples that can be access through pattern matching. Provides methods to query and manipulate the set.
    /// This class imposes lifo ordering on the underlying tuples.
    /// </summary>
    public sealed class PileSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the PileSpace class. All tuples will be created using the provided tuple factory;
        /// if none is provided the default TupleFactory will be used.
        /// </summary>
        public PileSpace(ITupleFactory tuplefactory = null) : base(tuplefactory ?? new TupleFactory())
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Returns the last index contained within the space to force lifo ordering.
        /// </summary>
        protected override int GetIndex(int size)
        {
            return 0;
        }

        #endregion
    }
}
