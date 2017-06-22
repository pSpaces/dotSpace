using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using Pong;

namespace PongServer
{
    public class Game
    {
        private AgentBase pong;
        private ISpace ts;
        private int height;
        private int width;

        public Game(int width, int height, ISpace ts)
        {
            this.ts = ts;
            this.width = width;
            this.height = height;
            this.pong = new PongController(ts, width, height);
        }

        public void Run()
        {
            this.ts.Put(EntityType.SIGNAL, "running", true);
            this.pong.Start();
            PlayerInfo leftplayer = (PlayerInfo)this.ts.Query(EntityType.PLAYERINFO, 1, typeof(string), typeof(int));
            PlayerInfo rightplayer = (PlayerInfo)this.ts.Query(EntityType.PLAYERINFO, 2, typeof(string), typeof(int));
            this.ts.Put(EntityType.SIGNAL, "serving", leftplayer.Name);
            this.ts.Put(EntityType.SIGNAL, "start");
        }

        public void Stop()
        {
            this.ts.Get(EntityType.SIGNAL, "running", true);
        }
    }
}
