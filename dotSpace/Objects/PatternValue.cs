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

        public PatternValue(string typename, object value)
        {
            this.TypeName = typename;
            this.Value = value;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public object TypeName { get; set; }

        public object Value { get; set; }

        #endregion
    }
}
