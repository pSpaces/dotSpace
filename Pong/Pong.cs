using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System.Windows;

namespace Pong
{
    /// <summary>
    /// Custom tuple, representing the pong domain object.
    /// </summary>
    public class Pong : ITuple
    {

        public Pong(string id, double x, double y, double directionx, double directiony, double speed)
        {
            this.Fields = new object[6];
            this.Fields[0] = EntityType.PONG;
            this.Fields[1] = x;
            this.Fields[2] = y;
            this.Fields[3] = directionx;
            this.Fields[4] = directiony;
            this.Fields[5] = speed;
        }

        public Pong(params object[] fields)
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


        public Vector Position
        {
            get { return new Vector((double)this.Fields[1], (double)this.Fields[2]); }
            set { this.Fields[1] = value.X; this.Fields[2] = value.Y; }
        }

        public Vector Direction
        {
            get { return new Vector((double)this.Fields[3], (double)this.Fields[4]); }
            set { this.Fields[3] = value.X; this.Fields[4] = value.Y; }
        }

        public double Speed
        {
            get { return (double)this.Fields[5]; }
            set { this.Fields[5] = value; }
        }
    }
}
