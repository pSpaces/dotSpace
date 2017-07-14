using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;

namespace dotSpace.Objects.Space
{

    /// <summary>
    /// Concrete implementation of a tuplespace datastructure.
    /// Represents a strongly typed set of tuples that can be access through pattern matching. Provides methods to query and manipulate the set.
    /// This class does not impose ordering on the underlying tuples, as they are inserted randomly into the underlying set.
    /// </summary>
    public sealed class RandomSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the FifoSpace class. All tuples will be created using the provided tuple factory;
        /// if none is provided the default TupleFactory will be used.
        /// </summary>
        public RandomSpace(ITupleFactory tuplefactory = null) : base(tuplefactory ?? new TupleFactory())
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Returns a random index of where the tuple must be inserted.
        /// </summary>
        protected override int GetIndex(int size)
        {
            return Environment.TickCount % (size+1);
        }

        #endregion
    }
}
