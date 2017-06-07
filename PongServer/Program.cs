using dotSpace.Enumerations;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;

namespace PongServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerNode server = new ServerNode(ConnectionMode.CONN, 123, "127.0.0.1");
            server.AddSpace("pong", new Space());
            int width = 80;
            int height = 25;
            Game pongGame = new Game(width, height, server["pong"]);
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
