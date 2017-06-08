using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Collections.Generic;

namespace dotSpace.Objects.Network.Messages.Responses
{
    public sealed class QueryAllResponse : BasicResponse, IEnumerableResult
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public QueryAllResponse()
        {
        }

        public QueryAllResponse(string source, string session, string target, IEnumerable<object[]> result, StatusCode code, string message) : base(ActionType.QUERYALL_RESPONSE, source, session, target, code, message)
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
