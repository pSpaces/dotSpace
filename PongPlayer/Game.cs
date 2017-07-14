using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using Pong;

namespace PongPlayer
{
    public class Game
    {
        private AIPlayer player;
        private View view;
        private ISpace ts;

        public Game(ISpace ts)
        {
            this.ts = ts;
            this.view = new View(ts);
        }

        public void SetPlayer(int playerId, string playername)
        {
            this.player = new AIPlayer(playerId, playername, this.ts);
        }

        public void Run()
        {
            this.view.Start();
            this.player.Start();
        }

        public void Stop()
        {
            this.ts.Get(EntityType.SIGNAL, "running", true);
        }
    }
}
