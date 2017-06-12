using dotSpace.BaseClasses;
using System;

namespace dotSpace.Objects.Spaces
{
    public sealed class RandomSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override int GetIndex(int size)
        {
            return Environment.TickCount % (size+1);
        }

        #endregion
    }
}
