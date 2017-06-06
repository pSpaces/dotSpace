using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    [DataContract]
    public class Binding
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Binding(string type)
        {
            this.TypeName = type;
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties


        [DataMember]
        public string TypeName { get; set; } 

        #endregion
    }
}
