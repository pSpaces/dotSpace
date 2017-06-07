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
            ClientNode client = new ClientNode(ConnectionMode.CONN, "127.0.0.1", 123);
            int width = 80;
            int height = 25;
            Game pongGame = new Game(width, height, client.GetRemoteSpace("pong"));
            pongGame.SetPlayer(playerId, "AI" + playerId);
            pongGame.Run();
            Console.ReadKey();
        }
    }
}
