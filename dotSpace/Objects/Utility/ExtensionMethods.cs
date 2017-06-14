using System;
using System.Collections.Generic;

namespace dotSpace.Objects.Utility
{
    /// <summary>
    /// Static collection of extension methods used to facilitate set operations.
    /// </summary>
    internal static class ExtensionMethods
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Applies a function on each element of the provided sequence.
        /// </summary>
        public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> a)
        {
            foreach (T t in enumerable)
            {
                a(t);
            }
        }

        #endregion
    }
}
