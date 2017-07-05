using dotSpace.Enumerations;
using dotSpace.Objects.Network;
using Pong;
using System;

namespace PongPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify player id [1|2]");
                Console.Read();
                return;
            }
            int playerId = int.Parse(args[0]);

            RemoteSpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/pong?KEEP", new EntityFactory());
            TerminalInfo.Initialize(80, 24);
            Game pongGame = new Game(remotespace);
            pongGame.SetPlayer(playerId, "AI" + playerId);
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
