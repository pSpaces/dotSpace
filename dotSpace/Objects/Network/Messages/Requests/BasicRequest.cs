using dotSpace.BaseClasses;
using dotSpace.Enumerations;

namespace dotSpace.Objects.Network.Messages.Requests
{
    public class BasicRequest : MessageBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors
        public BasicRequest()
        {
        }

        public BasicRequest(ActionType action, string source, string session, string target) : base(action, source, session, target)
        {
        }

        #endregion
    }
}
