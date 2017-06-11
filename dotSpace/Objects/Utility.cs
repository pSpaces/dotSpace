using dotSpace.Enumerations;
using System;
using System.Collections.Generic;

namespace dotSpace.Objects
{
    public static class Utility
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

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
