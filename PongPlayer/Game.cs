using dotSpace.Interfaces;
using Pong;
using Pong.BaseClasses;

namespace PongPlayer
{
    public class Game
    {
        private PlayerBase player;
        private View view;
        private ISpace ts;
        private int height;
        private int width;

        public Game(int width, int height, ISpace ts)
        {
            this.ts = ts;
            this.width = width;
            this.height = height;
            this.view = new View(width, height, ts);
        }

        public void SetPlayer(int playerId, string playername)
        {
            this.player = new AIPlayer(playerId, playername, this.width, this.height, this.ts);
        }

        public void Run()
        {
            this.view.Start();
            this.player.Start();
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
