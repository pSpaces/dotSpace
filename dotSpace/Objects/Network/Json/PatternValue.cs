namespace dotSpace.Objects.Json
{
    /// <summary>
    /// Entity used for describing interoperable value types when serializing and deserializing json objects.
    /// </summary>
    internal class PatternValue
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors
        
        /// <summary>
        /// Initializes a new instance of the PatternValue class.
        /// </summary>
        public PatternValue()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PatternValue class.
        /// </summary>
        public PatternValue(string typename, object value)
        {
            this.TypeName = typename;
            this.Value = value;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Textual representation of the underlying tuple element type.
        /// </summary>
        public object TypeName { get; set; }

        /// <summary>
        /// Generic representation of the underlying tuple element value.
        /// </summary>
        public object Value { get; set; }

        #endregion
    }
}
