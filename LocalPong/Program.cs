using dotSpace.Interfaces;
using dotSpace.Objects.Space;
using Pong;
using System;

namespace LocalPong
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpace ts = new FifoSpace();
            int width = 80;
            int height = 25;
            Game pongGame = new Game(width, height, ts);
            pongGame.AddPlayer(new AIPlayer(1, "AI1", width, height, ts));
            pongGame.AddPlayer(new AIPlayer(2, "AI2", width, height, ts));
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
