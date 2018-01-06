using dotSpace.BaseClasses.Network;
using dotSpace.Interfaces.Network;

namespace dotSpace.Objects.Network
{
    /// <summary>
    /// Concrete implementation of a space repository.
    /// Provides the basic functionality for supporting multiple distributed spaces. 
    /// It allows direct access to the contained spaces through their respective identifies.
    /// Additionally, it facilitates distributed access to the underlying spaces through Gates. 
    /// </summary>
    public sealed class SpaceRepository : RepositoryBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IOperationMap operationMap;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the SpaceRepository class.
        /// </summary>
        public SpaceRepository() : base()
        {
            this.operationMap = new OperationMap(this);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Processes the incoming connection using the internal operation map.
        /// </summary>
        protected override void OnConnect(IConnectionMode mode)
        {
            mode?.ProcessRequest(this.operationMap);
        }

        #endregion
    }
}
