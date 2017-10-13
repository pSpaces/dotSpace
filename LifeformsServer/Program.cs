using dotSpace.Objects.Network;
using dotSpace.Objects.Space;
using Lifeforms;
using System;

namespace LifeformsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceRepository repository = new SpaceRepository();
            repository.AddGate("tcp://127.0.0.1:123?KEEP");
            repository.AddSpace("lifeforms", new SequentialSpace(new EntityFactory()));
            TerminalInfo.Initialize(80, 24);
            Game lifeforms = new Game(repository.GetSpace("lifeforms"));
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
