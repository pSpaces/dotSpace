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
    [KnownType(typeof(MessageBase))]
    public abstract class ResponseBase : MessageBase
    {
        public ResponseBase(ActionType action, string source, string session, string target, int code, string message) : base(action, source, session, target)
        {
            this.Code = code;
            this.Message = message;
        }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Message { get; set; }

    }
}
