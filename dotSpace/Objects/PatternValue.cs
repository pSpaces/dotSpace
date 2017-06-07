using System.Runtime.Serialization;

namespace dotSpace.Objects
{
    public class PatternValue
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public PatternValue()
        {
        }

        public PatternValue(object value)
        {
            this.Value = value;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties


        [DataMember]
        public object Value { get; set; }

        #endregion
    }
}
