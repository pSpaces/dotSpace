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

//Put requests have the following format
//{ "mode": mode_code, "action": "PUT_REQUEST", "source" : source, "session": session, "target": target, "tuple" : tuple }

//Get requests have the following format
//{ "mode": mode_code, "action": request,       "source" : source, "session": session, "target": target, "template" : template }


//Put responses
//{ "action": "PUT_RESPONSE", "source" : source, "session": session, "target": target, "code" : code , "message": message }

//Get responses
//{ "action": response,       "source" : source, "session": session, "target": target, "result": result , "code" : code , "message": message }


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
        public int Code { get; set; }
        public string Message { get; set; }

    }
}
