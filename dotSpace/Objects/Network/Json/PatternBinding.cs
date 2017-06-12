using System.Runtime.Serialization;

namespace dotSpace.Objects.Json
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

        public string TypeName { get; set; } 

        #endregion
    }
}
