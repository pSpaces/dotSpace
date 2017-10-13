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
            repository.AddSpace("pong", new SequentialSpace(new EntityFactory()));
            TerminalInfo.Initialize(80, 24);
            Game pongGame = new Game(repository.GetSpace("pong"));
            pongGame.Run();
            Console.ReadKey();
            pongGame.Stop();
        }
    }
}
