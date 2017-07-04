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
            repository.AddSpace("lifeforms", new FifoSpace(new EntityFactory()));
            int width = Math.Min(80, Console.BufferWidth);
            int height = Math.Min(25, Console.BufferHeight);
            Game lifeforms = new Game(width, height, repository.GetSpace("lifeforms"));
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
