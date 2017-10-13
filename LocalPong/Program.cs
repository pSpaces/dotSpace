using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using Pong;
using System;

namespace LocalPong
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpace ts = new SequentialSpace(new EntityFactory());
            TerminalInfo.Initialize(80, 24);
            Game pongGame = new Game(ts);
            pongGame.AddPlayer(new AIPlayer(1, "AI1", ts));
            pongGame.AddPlayer(new AIPlayer(2, "AI2", ts));
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
