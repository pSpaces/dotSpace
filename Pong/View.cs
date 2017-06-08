using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Threading;

namespace Pong
{
    public class View : AgentBase
    {
        private char[,] screenBuffer;
        private readonly int width;
        private readonly int height;
        private string playerLeft;
        private string playerRight;
        private ConsoleColor currentColor;

        public View(int width, int height, ISpace ts) : base("view", ts)
        {
            this.width = width;
            this.height = height;
            this.screenBuffer = new char[this.width, this.height];
            Console.CursorVisible = false;
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.ts.Query("start");
            // Read the player names
            ITuple leftplayer = this.ts.Query(1, typeof(string));
            ITuple rightplayer = this.ts.Query(2, typeof(string));
            this.playerLeft = (string)leftplayer[1];
            this.playerRight = (string)rightplayer[1];

            // Keep iterating while the state is 'running'
            while (this.ts.Query("running", true) != null)
            {
                this.SetPongPosition();
                this.SetPlayerPosition(1);
                this.SetPlayerPosition(2);
                this.Show();
                Thread.Sleep(40); 
            }
        }

        private void SetPongPosition()
        {
            ITuple pong = this.ts.Query("pong", typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
            int x = (int)(double)pong[1];
            int y = (int)(double)pong[2];
            screenBuffer[x, y] = 'o';
        }

        private void SetPlayerPosition(int playerId)
        {
            ITuple playerPosition = this.ts.Query(playerId, typeof(double), typeof(double));
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

        private void SetForegroundColor(char c)
        {
            ConsoleColor color;

            if (c == 'o') color = ConsoleColor.Red;
            else if (c == '_') color = ConsoleColor.White;
            else if (c == '|') color = ConsoleColor.Green;
            else color = ConsoleColor.White;

            if (currentColor != color)
            {
                Console.ForegroundColor = color;
                currentColor = color;
            }
        }

        private void Show()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    this.SetForegroundColor(screenBuffer[x, y]);
                    Console.Write(screenBuffer[x, y]);
                }
            }
            Console.SetCursorPosition(0, this.height);
            Console.Write(this.ShowPlayerScores());
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    screenBuffer[x, y] = ' ';
                    if (y == 0 || y == this.height - 1)
                    {
                        screenBuffer[x, y] = '_';
                    }
                }
            }
        }
    }
}
