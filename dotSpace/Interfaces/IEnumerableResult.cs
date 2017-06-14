using System.Collections.Generic;

namespace dotSpace.Interfaces
{
    /// <summary>
    /// Defines a generalized set of results.
    /// </summary>
    public interface IEnumerableResult
    {

        /// <summary>
        /// Enumerable set containing the results.
        /// </summary>
        IEnumerable<object[]> Result { get; set; }
    }
}
