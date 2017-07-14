using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace dotSpace.Objects.Space
{
    /// <summary>
    /// Concrete implementation of a tuple factory using dotSpace's tuple class.
    /// Provides the primitives required to instantiate a tuple. 
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
