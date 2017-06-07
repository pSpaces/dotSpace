using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    public class PatternBinding
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PatternBinding()
        {
        }

        public PatternBinding(string type)
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
