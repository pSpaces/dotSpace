using dotSpace.Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

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

        public string GetData()
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
            ser.WriteObject(stream1, this);

            return Encoding.UTF8.GetString(stream1.GetBuffer());
        }
    }
}
