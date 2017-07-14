using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using Pong;
using System.Collections.Generic;

namespace LocalPong
{
    public class Game
    {
        private List<AIPlayer> players;
        private AgentBase pong;
        private View view;
        private ISpace ts;

        public Game(ISpace ts)
        {
            this.ts = ts;

            this.players = new List<AIPlayer>();
            this.pong = new PongController(ts);
            this.view = new View(ts);
        }

        public void AddPlayer(AIPlayer player)
        {
            this.players.Add(player);
        }

        public void Run()
        {
            this.ts.Put(EntityType.SIGNAL, "running", true);
            this.ts.Put(EntityType.SIGNAL, "serving", this.players[0].Name);
            this.view.Start();
            this.pong.Start();
            foreach (AIPlayer player in this.players)
            {
                player.Start();
            }
            this.ts.Put(EntityType.SIGNAL, "start");
        }

        public void Stop()
        {
            this.ts.Get(EntityType.SIGNAL, "running", true);
        }
    }
}
