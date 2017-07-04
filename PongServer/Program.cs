using dotSpace.Objects.Network;
using dotSpace.Objects.Space;
using Pong;
using System;

namespace PongServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceRepository repository = new SpaceRepository();
            repository.AddGate("tcp://127.0.0.1:123?KEEP");
            repository.AddSpace("pong", new FifoSpace(new EntityFactory()));
            int width = Math.Min(80, Console.BufferWidth);
            int height = Math.Min(25, Console.BufferHeight);
            Game pongGame = new Game(width, height, repository.GetSpace("pong"));
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
