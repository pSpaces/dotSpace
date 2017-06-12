using dotSpace.Objects.Network;
using dotSpace.Objects.Spaces;
using System;

namespace PongServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceRepository repository = new SpaceRepository();
            repository.AddGate("tcp://127.0.0.1:123?KEEP");
            repository.AddSpace("pong", new FifoSpace());
            int width = 80;
            int height = 25;
            Game pongGame = new Game(width, height, repository.GetSpace("pong"));
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
