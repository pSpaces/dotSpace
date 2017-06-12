using dotSpace.BaseClasses;

namespace dotSpace.Objects.Spaces
{
    public sealed class LifoSpace : SpaceBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override int GetIndex(int size)
        {
            return 0;
        }

        #endregion
    }
}
