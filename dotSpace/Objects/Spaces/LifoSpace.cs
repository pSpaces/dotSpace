using dotSpace.BaseClasses;

namespace dotSpace.Objects.Spaces
{
    /// <summary>
    /// Concrete implementation of a tuplespace datastructure.
    /// Represents a strongly typed set of tuples that can be access through pattern matching. Provides methods to query and manipulate the set.
    /// This class imposes lifo ordering on the underlying tuples.
    /// </summary>
    public sealed class LifoSpace : SpaceBase
    {
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
