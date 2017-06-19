using dotSpace.Interfaces;

namespace dotSpace.Objects.Spaces
{
    /// <summary>
    /// Concrete implementation of a tuple factory using dotSpace's tuple class.
    /// Provides the primitives required to define a tuple. 
    /// </summary>
    public sealed class TupleFactory : ITupleFactory
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of a TupleFactory class.
        /// </summary>
        public TupleFactory()
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Returns a new instance of the concrete tuple class in dotSpace.
        /// </summary>
        public ITuple Create(params object[] fields)
        {
            return new Tuple(fields);
        }

        #endregion
    }
}
