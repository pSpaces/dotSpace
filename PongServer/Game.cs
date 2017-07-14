using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using Pong;

namespace PongServer
{
    public class Game
    {
        private AgentBase pong;
        private ISpace ts;

        public Game(ISpace ts)
        {
            this.ts = ts;
            this.pong = new PongController(ts);
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
