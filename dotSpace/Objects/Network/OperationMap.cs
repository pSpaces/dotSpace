using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Network;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using dotSpace.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotSpace.Objects.Network
{
    /// <summary>
    /// Concrete implementation of the IOperationMap interface.
    /// Provides basic functionality to map requests with operations on a space repository.
    /// </summary>
    internal sealed class OperationMap : IOperationMap
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private IRepository repository;
        private Dictionary<Type, Func<IMessage, IMessage>> operationMap;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instances of the OperationMap class.
        /// </summary>
        public OperationMap(IRepository repository)
        {
            this.repository = repository;
            this.operationMap = new Dictionary<Type, Func<IMessage, IMessage>>();
            this.operationMap.Add(typeof(GetRequest), this.PerformGet);
            this.operationMap.Add(typeof(GetPRequest), this.PerformGetP);
            this.operationMap.Add(typeof(GetAllRequest), this.PerformGetAll);
            this.operationMap.Add(typeof(QueryRequest), this.PerformQuery);
            this.operationMap.Add(typeof(QueryPRequest), this.PerformQueryP);
            this.operationMap.Add(typeof(QueryAllRequest), this.PerformQueryAll);
            this.operationMap.Add(typeof(PutRequest), this.PerformPut);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Executes an operation defined within the request. Followingly, the response is returned.
        /// </summary>
        public IMessage Execute(IMessage request)
        {
            Type requestType = request.GetType();
            if (this.operationMap.ContainsKey(requestType))
            {
                return this.operationMap[requestType](request);
            }

            return new BasicResponse(request.Actiontype, request.Source, request.Session, request.Target, StatusCode.METHOD_NOT_ALLOWED, StatusMessage.METHOD_NOT_ALLOWED);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private IMessage PerformGet(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                GetRequest getReq = (GetRequest)request;
                ITuple tuple = ts.Get(new Pattern(getReq.Template));
                return new GetResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new GetResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private IMessage PerformGetP(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                GetPRequest getReq = (GetPRequest)request;
                ITuple tuple = ts.GetP(new Pattern(getReq.Template));
                return new GetPResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new GetPResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private IMessage PerformGetAll(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                GetAllRequest getReq = (GetAllRequest)request;
                IEnumerable<ITuple> tuples = ts.GetAll(new Pattern(getReq.Template));
                return new GetAllResponse(request.Source, request.Session, request.Target, tuples?.Select(x => x.Fields) ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new GetAllResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private IMessage PerformQuery(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                QueryRequest getReq = (QueryRequest)request;
                ITuple tuple = ts.Query(new Pattern(getReq.Template));
                return new QueryResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new QueryResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private IMessage PerformQueryP(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                QueryPRequest getReq = (QueryPRequest)request;
                ITuple tuple = ts.QueryP(new Pattern(getReq.Template));
                return new QueryPResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new QueryPResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private IMessage PerformQueryAll(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                QueryAllRequest getReq = (QueryAllRequest)request;
                IEnumerable<ITuple> tuples = ts.QueryAll(new Pattern(getReq.Template));
                return new QueryAllResponse(request.Source, request.Session, request.Target, tuples?.Select(x => x.Fields) ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new QueryAllResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private IMessage PerformPut(IMessage request)
        {
            ISpace ts = this.repository.GetSpace(request.Target);
            if (ts != null)
            {
                PutRequest putReq = (PutRequest)request;
                ts.Put(new Objects.Space.Tuple(putReq.Tuple));
                return new PutResponse(request.Source, request.Session, request.Target, StatusCode.OK, StatusMessage.OK);
            }
            return new PutResponse(request.Source, request.Session, request.Target, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        } 

        #endregion
    }
}
