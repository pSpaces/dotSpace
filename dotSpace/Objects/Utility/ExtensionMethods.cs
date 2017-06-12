using System;
using System.Collections.Generic;

namespace dotSpace.Objects.Utility
{
    public static class ExtensionMethods
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
