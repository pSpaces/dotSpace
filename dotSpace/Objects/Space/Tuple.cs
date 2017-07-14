using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;
using System.Linq;
using System.Text;

namespace dotSpace.Objects.Space
{
    /// <summary>
    /// Concrete implementation of a tuple.
    /// Provides the primitives required to define a tuple. 
    /// </summary>
    public sealed class Tuple : ITuple
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of a Tuple class.
        /// </summary>
        public Tuple(params object[] fields)
        {
            this.Fields = fields;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        /// <summary>
        /// Gets or sets the underlying array of values representing the tuple.
        /// </summary>
        public object[] Fields { get; set; }

        /// <summary>
        /// Returns the size of the tuple.
        /// </summary>
        public int Size { get { return Fields.Length; } }

        /// <summary>
        /// Gets or sets the i'th element of the tuple.
        /// </summary>
        public object this[int idx] { get { return this.Fields[idx]; } set { this.Fields[idx] = value; } }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Returns a textual representation of the underlying tuple values.
        /// </summary>
        public override string ToString()
        {
            return string.Format("<{0}>", String.Join(",", this.Fields.Select(x=>x.ToString())));
        }

        #endregion
    };
}
