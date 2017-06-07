using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using Pong;
using Pong.BaseClasses;
using System.Collections.Generic;

namespace LocalPong
{
    public class Game
    {
        private List<PlayerBase> players;
        private AgentBase pong;
        private View view;
        private ISpace ts;
        private int height;
        private int width;

        public Game(int width, int height, ISpace ts)
        {
            this.ts = ts;
            this.width = width;
            this.height = height;
            this.players = new List<PlayerBase>();
            this.pong = new Pong.Pong(ts, width, height);
            this.view = new View(width, height, ts);
        }

        public void AddPlayer(PlayerBase player)
        {
            this.players.Add(player);
        }

        public void Run()
        {
            this.ts.Put("running", true);
            this.ts.Put("serving", this.players[0].Name);
            this.view.Start();
            this.pong.Start();
            foreach (PlayerBase player in this.players)
            {
                player.Start();
            }
            this.ts.Put("start");
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
