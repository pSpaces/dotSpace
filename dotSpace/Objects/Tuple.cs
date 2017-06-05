using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    [DataContract(Namespace = "")]
    public class Tuple : ITuple
    {
        [DataMember]
        public object[] Fields { get; set; }

        public Tuple(params object[] fields)
        {
            this.Fields = fields;

        }

        public int Size { get { return Fields.Length; } }

        public object this[int idx] { get { return this.Fields[idx]; } }


    };
}
