using dotSpace.Interfaces;
using dotSpace.Objects;
using Pong;
using System;

namespace LocalPong
{
    class Program
    {
        static void Main(string[] args)
        {
            ITupleSpace ts = new TupleSpace();
            int width = 80;
            int height = 25;
            Game pongGame = new Game(80, 25, ts);
            pongGame.AddPlayer(new AIPlayer(Player.Left, "AI1", width, height, ts));
            pongGame.AddPlayer(new AIPlayer(Player.Right, "AI2", width, height, ts));
            pongGame.Run();

            Console.ReadKey();
        }
    }
}
