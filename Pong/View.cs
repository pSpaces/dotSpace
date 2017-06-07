using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Threading;

namespace Pong
{
    public class View : Agent
    {
        public char[,] screenBuffer;
        private readonly int width;
        private readonly int height;
        private string playerLeft;
        private string playerRight;

        public View(int width, int height, ITupleSpace ts) : base("view", ts)
        {
            this.width = width;
            this.height = height;
            this.screenBuffer = new char[this.width, this.height];
            Console.CursorVisible = false;
        }

        protected override void DoWork()
        {
            ITuple leftplayer = this.ts.Query(Player.Left, typeof(string));
            ITuple rightplayer = this.ts.Query(Player.Right, typeof(string));
            this.playerLeft = (string)leftplayer[1];
            this.playerRight = (string)rightplayer[1];
            while (this.ts.Query("running", true) != null)
            {
                this.SetPongPosition();
                this.SetPlayerPosition(Player.Left);
                this.SetPlayerPosition(Player.Right);
                this.Show();
                Thread.Sleep(1);
            }
        }

        private void SetPongPosition()
        {
            ITuple pong = this.ts.Query("pong", typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
            int x = (int)(double)pong[1];
            int y = (int)(double)pong[2];
            screenBuffer[x, y] = 'o';
        }

        private void SetPlayerPosition(Player playerId)
        {
            ITuple playerPosition  = this.ts.Query(playerId, typeof(double), typeof(double));
            int x = (int)(double)playerPosition[1];
            int y = (int)(double)playerPosition[2];
            screenBuffer[x, y] = '|';
        }

        private string ShowPlayerScores()
        {
            ITuple playerAScore = this.ts.Query(this.playerLeft, typeof(int));
            int scoreA = (int)playerAScore[1];
            ITuple playerBScore = this.ts.Query(this.playerRight, typeof(int));
            int scoreB = (int)playerBScore[1];
            return string.Format("{0}: {1} - {2}: {3}", playerLeft, scoreA, playerRight, scoreB);
        }

        public void Show()
        {
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(screenBuffer[x, y]);
                }
            }
            Console.SetCursorPosition(0, this.height);
            Console.Write(this.ShowPlayerScores());
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    if (x == 0 || x == this.width - 1)
                    {
                        screenBuffer[x, y] = '#';
                    }
                    else
                    {
                        screenBuffer[x, y] = ' ';
                    }
                    if (y == 0 || y == this.height - 1)
                    {
                        screenBuffer[x, y] = '#';
                    }
                }
            }
        }
    }
}
