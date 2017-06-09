using dotSpace.Interfaces;

namespace dotSpace.Objects
{
    public class Pattern : IPattern
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Pattern(params object[] fields)
        {
            this.Fields = fields;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Properties

        public object[] Fields { get; set; }

        public int Size { get { return Fields.Length; } }

        public object this[int idx] { get { return this.Fields[idx]; } }

        #endregion

    };
}
