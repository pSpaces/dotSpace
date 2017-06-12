using dotSpace.BaseClasses;

namespace dotSpace.Objects.Spaces
{
    public sealed class FifoSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override int GetIndex(int size)
        {
            return size;
        }

        #endregion
    }
}
