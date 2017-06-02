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
    [KnownType(typeof(RequestBase))]
    public class GetResponse : ResponseBase
    {
        public GetResponse(string source, string session, string target, IFields result, int code, string message) : base(ActionType.GET_RESPONSE, source, session, target, code, message)
        {
            this.Result = result.Fields;
        }

        [DataMember]
        public object[] Result { get; set; }
    }
}
