using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    [DataContract(Namespace = "")]
    public class Pattern : IPattern
    {
        public Pattern(params object[] fields)
        {
            this.Fields = fields;
        }

        [DataMember]
        public object[] Fields { get; set; }

        public int Size { get { return Fields.Length; } }

        public object this[int idx] { get { return this.Fields[idx]; } }

    };
}
