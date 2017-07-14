using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace Pong
{
    /// <summary>
    /// Custom tuple, representing the player information domain object.
    /// </summary>
    public class PlayerInfo : ITuple
    {

        public PlayerInfo(int id, string name, int score)
        {
            this.Fields = new object[4];
            this.Fields[0] = EntityType.PLAYERINFO;
            this.Id = id;
            this.Name = name;
            this.Score = score;
        }

        public PlayerInfo(params object[] fields)
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

        public int Id
        {
            get { return (int)this.Fields[1]; }
            set { this.Fields[1] = value; }
        }
        public string Name
        {
            get { return (string)this.Fields[2]; }
            set { this.Fields[2] = value; }
        }
        public int Score
        {
            get { return (int)this.Fields[3]; }
            set { this.Fields[3] = value; }
        }

    }
}
