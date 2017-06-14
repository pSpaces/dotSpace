namespace dotSpace.Objects.Json
{
    /// <summary>
    /// Entity used for describing interoperable binding types when serializing and deserializing json objects.
    /// </summary>
    internal class PatternBinding
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the PatternBinding class.
        /// </summary>
        public PatternBinding()
        {
        }
        /// <summary>
        /// Initializes a new instance of the PatternBinding class.
        /// </summary>
        public PatternBinding(string type)
        {
            this.TypeName = type;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Textual representation of the underlying pattern element type.
        /// </summary>
        public string TypeName { get; set; } 

        #endregion
    }
}
