using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System.Collections.Generic;

namespace dotSpace.Objects.Network.Messages.Responses
{
    /// <summary>
    /// Entity representing a message of a QueryAll response.
    /// </summary>
    public sealed class QueryAllResponse : BasicResponse, IEnumerableResult
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the QueryAllResponse class.
        /// </summary>
        public QueryAllResponse()
        {
        }

        /// <summary>
        /// Initializes a new instances of the QueryAllResponse class.
        /// </summary>
        public QueryAllResponse(string source, string session, string target, IEnumerable<object[]> result, StatusCode code, string message) : base(ActionType.QUERYALL_RESPONSE, source, session, target, code, message)
        {
            this.Result = result;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the enumerable set containing the results.
        /// </summary>
        public IEnumerable<object[]> Result { get; set; }

        #endregion
    }
}
