using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;

namespace Lifeforms
{
    /// <summary>
    /// Custom tuple, representing the stats of a lifeform domain object.
    /// </summary>
    public class LifeformStats : ITuple
    {
        public LifeformStats(string id, int maxSpeedgain, SpawnLifeform spawn)
        {
            this.Fields = new object[11];
            this.Fields[0] = EntityType.LIFEFORM_STATS;

            this.Id = id;
            this.Genom = spawn.Genom;
            this.P1Genom = spawn.P1Genom;
            this.P2Genom = spawn.P2Genom;
            this.Life = spawn.Life;
            this.Generation = spawn.Generation;
            this.VisualRange = spawn.VisualRange;
            this.MaxNrChildren = spawn.MaxNrChildren;
            this.Speed = Math.Min(maxSpeedgain, spawn.Speed);
            this.InitialLife = spawn.Life;
        }

        public LifeformStats(params object[] fields)
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
        public long Genom
        {
            get { return (long)this.Fields[2]; }
            set { this.Fields[2] = value; }
        }
        public long P1Genom
        {
            get { return (long)this.Fields[3]; }
            set { this.Fields[3] = value; }
        }
        public long P2Genom
        {
            get { return (long)this.Fields[4]; }
            set { this.Fields[4] = value; }
        }
        public int Life
        {
            get { return (int)this.Fields[5]; }
            set { this.Fields[5] = value; }
        }

        public int Generation
        {
            get { return (int)this.Fields[6]; }
            set { this.Fields[6] = value; }
        }
        public int VisualRange
        {
            get { return (int)this.Fields[7]; }
            set { this.Fields[7] = value; }
        }
        public int MaxNrChildren
        {
            get { return (int)this.Fields[8]; }
            set { this.Fields[8] = value; }
        }
        public int Speed
        {
            get { return (int)this.Fields[9]; }
            set { this.Fields[9] = value; }
        }
        public int InitialLife
        {
            get { return (int)this.Fields[10]; }
            set { this.Fields[10] = value; }
        }

    }
}
