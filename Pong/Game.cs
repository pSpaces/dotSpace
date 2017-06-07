using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using Pong.BaseClasses;
using System.Collections.Generic;

namespace Pong
{
    public class Game
    {
        private char[,] court;

        private List<PlayerBase> players;
        private Agent pong;
        private View view;
        private ITupleSpace ts;
        private int height;
        private int width;

        public Game(int width, int height, ITupleSpace ts)
        {
            this.ts = ts;
            this.width = width;
            this.height = height;
            this.players = new List<PlayerBase>();
            this.court = new char[width, height];
            this.pong = new Pong(ts, width, height);
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
        }

        public void Stop()
        {
            this.ts.Get("running", true);
        }
    }
}
