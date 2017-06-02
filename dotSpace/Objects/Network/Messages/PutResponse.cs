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
    public class PutResponse : ResponseBase
    {
        public PutResponse(string source, string session, string target, int code, string message) : base(ActionType.PUT_RESPONSE, source, session, target, code, message)
        {
        }
    }
}
