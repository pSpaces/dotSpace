using dotSpace.BaseClasses;
using dotSpace.Interfaces;

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
            this.pong = new Pong.Pong(ts, width, height);
        }

        public void Run()
        {
            this.ts.Put("running", true);
            this.pong.Start();
            ITuple leftplayer = this.ts.Query(1, typeof(string));
            ITuple rightplayer = this.ts.Query(2, typeof(string));
            this.ts.Put("serving", leftplayer[1]);
            this.ts.Put("start");
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
