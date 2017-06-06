using dotSpace.Interfaces;
using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    [DataContract(Namespace = "")]
    public sealed class Tuple : ITuple
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Tuple(params object[] fields)
        {
            this.Fields = fields;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        [DataMember]
        public object[] Fields { get; set; }

        public int Size { get { return Fields.Length; } }

        public object this[int idx] { get { return this.Fields[idx]; } set { this.Fields[idx] = value; } } 

        #endregion
    };
}
