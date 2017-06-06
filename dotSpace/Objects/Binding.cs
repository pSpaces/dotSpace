using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    [DataContract]
    public class Binding
    {
        public Binding(string type)
        {
            this.TypeName = type;
        }

        [DataMember]
        public string TypeName { get; set; }
    }
}
