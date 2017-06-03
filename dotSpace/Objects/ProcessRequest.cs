using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotSpace.Objects
{
    class ProcessRequest
    {
        private ITupleSpace tuplespace;

        public ProcessRequest(ITupleSpace tuplespace)
        {
            this.tuplespace = tuplespace;
        }


        public void Execute(BasicRequest request)
        {
            if (request is PutRequest)
            {
                PutRequest req = (PutRequest)request;
                Tuple tuple = new Tuple(req.Tuple);
                this.tuplespace.Put(tuple);
            }
        }
    }
}
