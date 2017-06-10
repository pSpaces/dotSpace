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
            SpaceRepository repository = new SpaceRepository(ConnectionMode.CONN, 123, "127.0.0.1");
            repository.AddSpace("pong", new Space());
            int width = 80;
            int height = 25;
            Game pongGame = new Game(width, height, repository.GetSpace("pong"));
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
