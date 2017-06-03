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
    public class GetRequest : BasicRequest
    {
        public GetRequest(ConnectionMode mode, string source, string session, string target, IPattern template) : base(mode, ActionType.GET_REQUEST, source, session, target)
        {
            this.Template = template.Fields;
        }

        [DataMember]
        public object[] Template { get; set; }
    }
}
