using dotSpace.Enumerations;
using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace dotSpace.Objects.Network
{
    [DataContract]
    [KnownType(typeof(BasicRequest))]
    public class PutRequest : BasicRequest
    {
        public PutRequest(ConnectionMode mode, string source, string session, string target, ITuple tuple) : base(mode, ActionType.PUT_REQUEST, source, session, target)
        {
            this.Tuple = tuple.Fields;
        }

        [DataMember]
        public object[] Tuple { get; set; }
    }
}
