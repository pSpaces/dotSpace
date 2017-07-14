using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace Pong
{
    /// <summary>
    /// Custom tuple, representing the position domain object.
    /// </summary>
    public class Position : ITuple
    {

        public Position(string id, double x, double y)
        {
            this.Fields = new object[4];
            this.Fields[0] = EntityType.POSITION;
            this.Id = id;
            this.X = x;
            this.Y = y;
        }

        public Position(params object[] fields)
        {
            this.Fields = fields;
        }

        public object this[int idx]
        {
            get { return this.Fields[idx]; }
            set { this.Fields[idx] = value; }
        }
        public object[] Fields { get; set; }
        public int Size { get { return this.Fields.Length; } }

        public string Id
        {
            get { return (string)this.Fields[1]; }
            set { this.Fields[1] = value; }
        }
        public double X
        {
            get { return (double)this.Fields[2]; }
            set { this.Fields[2] = value; }
        }
        public double Y
        {
            get { return (double)this.Fields[3]; }
            set { this.Fields[3] = value; }
        }

    }
}
