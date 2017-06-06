using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;
using System.Linq;
using System.Collections.Generic;

namespace dotSpace.Objects
{
    public class TSOperationMap
    {
        private ServerNode serverNode;
        private Dictionary<Type, Func<BasicRequest, BasicResponse>> operationMap;

        public TSOperationMap(ServerNode serverNode)
        {
            this.serverNode = serverNode;
            this.operationMap = new Dictionary<Type, Func<BasicRequest, BasicResponse>>();
            this.operationMap.Add(typeof(GetRequest), this.PerformGet);            
            this.operationMap.Add(typeof(GetPRequest), this.PerformGetP);
            this.operationMap.Add(typeof(GetAllRequest), this.PerformGetAll);
            this.operationMap.Add(typeof(QueryRequest), this.PerformQuery);
            this.operationMap.Add(typeof(QueryPRequest), this.PerformQueryP);
            this.operationMap.Add(typeof(QueryAllRequest), this.PerformQueryAll);
            this.operationMap.Add(typeof(PutRequest), this.PerformPut);
        }

        public BasicResponse Execute(BasicRequest request)
        {
            Type requestType = request.GetType();
            if (this.operationMap.ContainsKey(requestType))
            {
                return this.operationMap[requestType](request);
            }

            return new BasicResponse(request.Action, request.Source, request.Session, request.Target, StatusCode.METHOD_NOT_ALLOWED, StatusMessage.METHOD_NOT_ALLOWED);
        }

        private BasicResponse PerformGet(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                GetRequest getReq = (GetRequest)request;
                ITuple tuple = ts.Get(new Pattern(getReq.Template));
                return new GetResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new GetResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private BasicResponse PerformGetP(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                GetPRequest getReq = (GetPRequest)request;
                ITuple tuple = ts.GetP(new Pattern(getReq.Template));
                return new GetPResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new GetPResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private BasicResponse PerformGetAll(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                GetAllRequest getReq = (GetAllRequest)request;
                IEnumerable<ITuple> tuples = ts.GetAll(new Pattern(getReq.Template));
                return new GetAllResponse(request.Source, request.Session, request.Target, tuples?.Select(x=>x.Fields) ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new GetAllResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private BasicResponse PerformQuery(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                QueryRequest getReq = (QueryRequest)request;
                ITuple tuple = ts.Query(new Pattern(getReq.Template));
                return new QueryResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new QueryResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private BasicResponse PerformQueryP(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                QueryPRequest getReq = (QueryPRequest)request;
                ITuple tuple = ts.QueryP(new Pattern(getReq.Template));
                return new QueryPResponse(request.Source, request.Session, request.Target, tuple?.Fields ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new QueryPResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private BasicResponse PerformQueryAll(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                QueryAllRequest getReq = (QueryAllRequest)request;
                IEnumerable<ITuple> tuples = ts.QueryAll(new Pattern(getReq.Template));
                return new QueryAllResponse(request.Source, request.Session, request.Target, tuples?.Select(x => x.Fields) ?? null, StatusCode.OK, StatusMessage.OK);
            }
            return new QueryAllResponse(request.Source, request.Session, request.Target, null, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
        private BasicResponse PerformPut(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                PutRequest putReq = (PutRequest)request;
                ts.Put(new Tuple(putReq.Tuple));
                return new PutResponse(request.Source, request.Session, request.Target, StatusCode.OK, StatusMessage.OK);
            }
            return new PutResponse(request.Source, request.Session, request.Target, StatusCode.NOT_FOUND, StatusMessage.NOT_FOUND);
        }
    }
}
