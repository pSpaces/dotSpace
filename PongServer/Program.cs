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
            Node node = new Node(ConnectionMode.CONN, 123, "127.0.0.1");
            node.AddSpace("pong", new Space());
            int width = 80;
            int height = 25;
            Game pongGame = new Game(width, height, node["pong"]);
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
