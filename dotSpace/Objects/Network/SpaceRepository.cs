using dotSpace.BaseClasses;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network
{
    public sealed class SpaceRepository : RepositoryBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private OperationMap operationMap;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public SpaceRepository() : base()
        {
            this.operationMap = new OperationMap(this);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        protected override void OnConnect(IConnectionMode mode)
        {
            mode?.ProcessRequest(this.operationMap);
        }

        #endregion
    }
}
