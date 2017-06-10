
using dotSpace.Enumerations;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;

namespace LifeformsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceRepository repository = new SpaceRepository(ConnectionMode.CONN, 123, "127.0.0.1");
            repository.AddSpace("lifeforms", new Space());
            int width = 80;
            int height = 25;
            Game lifeforms = new Game(width, height, repository.GetSpace("lifeforms"));
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
