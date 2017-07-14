using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace Lifeforms
{
    /// <summary>
    /// Custom tuple, representing the offspring domain object.
    /// </summary>
    public class SpawnLifeform : ITuple
    {
        public SpawnLifeform(long genom, long p1genom, long p2genom, int life, int food, int x, int y, int generation, int visualRange, int maxNrChildren, int speed)
        {
            this.Fields = new object[12];
            this.Fields[0] = EntityType.SPAWN;

            this.Genom = genom;
            this.P1Genom = p1genom;
            this.P2Genom = p2genom;
            this.Life = life;
            this.Food = food;
            this.X = x;
            this.Y = y;
            this.Generation = generation;
            this.VisualRange = visualRange;
            this.MaxNrChildren = maxNrChildren;
            this.Speed = speed;
        }

        public SpawnLifeform(params object[] fields)
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


        public long Genom
        {
            get { return (long)this.Fields[1]; }
            set { this.Fields[1] = value; }
        }
        public long P1Genom
        {
            get { return (long)this.Fields[2]; }
            set { this.Fields[2] = value; }
        }
        public long P2Genom
        {
            get { return (long)this.Fields[3]; }
            set { this.Fields[3] = value; }
        }
        public int Life
        {
            get { return (int)this.Fields[4]; }
            set { this.Fields[4] = value; }
        }
        public int Food
        {
            get { return (int)this.Fields[5]; }
            set { this.Fields[5] = value; }
        }
        public int X
        {
            get { return (int)this.Fields[6]; }
            set { this.Fields[6] = value; }
        }
        public int Y
        {
            get { return (int)this.Fields[7]; }
            set { this.Fields[7] = value; }
        }
        public int Generation
        {
            get { return (int)this.Fields[8]; }
            set { this.Fields[8] = value; }
        }
        public int VisualRange
        {
            get { return (int)this.Fields[9]; }
            set { this.Fields[9] = value; }
        }
        public int MaxNrChildren
        {
            get { return (int)this.Fields[10]; }
            set { this.Fields[10] = value; }
        }
        public int Speed
        {
            get { return (int)this.Fields[11]; }
            set { this.Fields[11] = value; }
        }

    }
}
