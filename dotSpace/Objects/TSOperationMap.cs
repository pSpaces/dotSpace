using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using System;
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
            this.operationMap.Add(typeof(PutRequest), this.PerformPut);
            this.operationMap.Add(typeof(GetRequest), this.PerformGet);
            this.operationMap.Add(typeof(GetPRequest), this.PerformGetP);
            this.operationMap.Add(typeof(QueryRequest), this.PerformQuery);
            this.operationMap.Add(typeof(QueryPRequest), this.PerformQueryP);
        }

        public BasicResponse Execute(BasicRequest request)
        {
            Type requestType = request.GetType();
            if (this.operationMap.ContainsKey(requestType))
            {
                return this.operationMap[requestType](request);
            }

            return new BasicResponse(request.Action, request.Source, request.Session, request.Target, 400, "Unknown operation");
        }
        private BasicResponse PerformPut(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                PutRequest putReq = (PutRequest)request;
                ts.Put(new Tuple(putReq.Tuple));
                return new PutResponse(request.Source, request.Session, request.Target, 200, "OK");
            }
            return new PutResponse(request.Source, request.Session, request.Target, 404, "Unknown target");
        }
        private BasicResponse PerformGet(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                GetRequest getReq = (GetRequest)request;
                ITuple tuple = ts.Get(new Pattern(getReq.Template));
                return new GetResponse(request.Source, request.Session, request.Target, tuple, 200, "OK");
            }
            return new GetResponse(request.Source, request.Session, request.Target, null, 404, "Unknown target");
        }
        private BasicResponse PerformGetP(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                GetPRequest getReq = (GetPRequest)request;
                ITuple tuple = ts.GetP(new Pattern(getReq.Template));
                return new GetPResponse(request.Source, request.Session, request.Target, tuple, 200, "OK");
            }
            return new GetPResponse(request.Source, request.Session, request.Target, null, 404, "Unknown target");
        }
        private BasicResponse PerformQuery(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                QueryRequest getReq = (QueryRequest)request;
                ITuple tuple = ts.Query(new Pattern(getReq.Template));
                return new QueryResponse(request.Source, request.Session, request.Target, tuple, 200, "OK");
            }
            return new QueryResponse(request.Source, request.Session, request.Target, null, 404, "Unknown target");
        }
        private BasicResponse PerformQueryP(BasicRequest request)
        {
            ITupleSpace ts = this.serverNode[request.Target];
            if (ts != null)
            {
                QueryPRequest getReq = (QueryPRequest)request;
                ITuple tuple = ts.QueryP(new Pattern(getReq.Template));
                return new QueryPResponse(request.Source, request.Session, request.Target, tuple, 200, "OK");
            }
            return new QueryPResponse(request.Source, request.Session, request.Target, null, 404, "Unknown target");
        }
    }
}
