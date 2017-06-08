using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Collections.Generic;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class GetAllResponse : BasicResponse, IEnumerableResult
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public GetAllResponse()
        {
        }

        public GetAllResponse(string source, string session, string target, IEnumerable<object[]> result, StatusCode code, string message) : base(ActionType.GETALL_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public IEnumerable<object[]> Result { get; set; }

        #endregion
    }
}
