using dotSpace.Enumerations;
using System;
using System.Runtime.Serialization;

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
    public abstract class MessageBase
    {
        public MessageBase(ActionType action, string source, string session, string target)
        {
            this.Action = action;
            this.Source = source;
            this.Session = session;
            this.Target = target;
        }

        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Session { get; set; }
        [DataMember]
        public string Target { get; set; }

        public ActionType Action { get; set; }

        [DataMember(Name = "Action")]
        string ActionString
        {
            get { return this.Action.ToString(); }
            set
            {
                ActionType action;
                this.Action = Enum.TryParse(value, true, out action) ? action : ActionType.NONE;
            }
        }


    }
}
