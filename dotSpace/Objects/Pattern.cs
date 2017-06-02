using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    [DataContract(Namespace = "")]
    public class Pattern : IPattern
    {
        [DataMember]
        public object[] Fields { get; set; }

        public Pattern(params object[] fields)
        {
            this.Fields = fields;
        }

        public bool Match(IFields entity)
        {
            object[] elements = entity.Fields;
            if (this.Fields.Length != elements.Length)
            {
                return false;
            }
            bool result = true;
            for (int idx = 0; idx < elements.Length; idx++)
            {
                if (this.Fields[idx] != null)
                {
                    result &= elements[idx].Equals(this.Fields[idx]);
                }
                else
                {
                    ///result &= elements[idx] is Type ? (Type)elements[idx].GetType() == (Type)this.Fields[idx] : elements[idx].GetType() == (Type)this.Fields[idx];
                }
            }

            return result;
        }

    };
}
