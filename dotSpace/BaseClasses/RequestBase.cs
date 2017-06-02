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
    public abstract class RequestBase : MessageBase
    {
        public RequestBase(ConnectionMode mode, ActionType action, string source, string session, string target) : base(action, source, session, target)
        {
            this.Mode = mode;

        }
        public ConnectionMode Mode { get; set; }
        [DataMember(Name = "Mode")]
        string ModeString
        {
            get { return this.Mode.ToString(); }
            set
            {
                ConnectionMode mode;
                this.Mode = Enum.TryParse(value, true, out mode) ? mode : ConnectionMode.NONE;
            }
        }


    }
}
