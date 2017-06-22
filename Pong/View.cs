using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Threading;

namespace Pong
{
    /// <summary>
    /// This class is a console based presenter.
    /// </summary>
    public class View : AgentBase
    {
        private char[,] screenBuffer;
        private readonly int width;
        private readonly int height;
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
            this.Query(EntityType.SIGNAL, "start");

            // Keep iterating while the state is 'running'
            while (this.Query(EntityType.SIGNAL, "running", true) != null)
            {
                this.SetPongPosition();
                this.SetPlayerPosition(1);
                this.SetPlayerPosition(2);
                this.Show();
                Thread.Sleep(25);
            }
        }

        private void SetPongPosition()
        {
            Pong pong = (Pong)this.Query(EntityType.PONG, typeof(double), typeof(double), typeof(double), typeof(double), typeof(double));
            int x = (int)pong.Position.X;
            int y = (int)pong.Position.Y;
            screenBuffer[x, y] = 'o';
        }

        private void SetPlayerPosition(int playerId)
        {
            Position playerPosition = (Position)this.Query(EntityType.POSITION, playerId, typeof(double), typeof(double));
            int x = (int)playerPosition.X;
            int y = (int)playerPosition.Y;
            screenBuffer[x, y] = '|';
        }

        private string ShowPlayerScores()
        {
            PlayerInfo leftplayer = (PlayerInfo)this.Query(EntityType.PLAYERINFO, 1, typeof(string), typeof(int));
            PlayerInfo rightplayer = (PlayerInfo)this.Query(EntityType.PLAYERINFO, 2, typeof(string), typeof(int));
            return string.Format("{0}: {1} - {2}: {3}", leftplayer.Name, leftplayer.Score, rightplayer.Name, rightplayer.Score);
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
