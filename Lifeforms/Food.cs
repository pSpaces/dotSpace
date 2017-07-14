using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace Lifeforms
{
    /// <summary>
    /// Custom tuple, representing the food domain object.
    /// </summary>
    public class Food : ITuple
    {
        public Food(string id, int amount, int timeleft, int x, int y)
        {
            this.Fields = new object[5];
            this.Fields[0] = EntityType.POSITION;
            this.Amount = amount;
            this.TimeLeft = timeleft;
            this.X = x;
            this.Y = y;
        }

        public Food(params object[] fields)
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

        public int Amount
        {
            get { return (int)this.Fields[1]; }
            set { this.Fields[1] = value; }
        }

        public int TimeLeft
        {
            get { return (int)this.Fields[2]; }
            set { this.Fields[2] = value; }
        }

        public int X
        {
            get { return (int)this.Fields[3]; }
            set { this.Fields[3] = value; }
        }
        public int Y
        {
            get { return (int)this.Fields[4]; }
            set { this.Fields[4] = value; }
        }

    }
}
