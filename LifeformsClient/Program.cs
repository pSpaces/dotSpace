using dotSpace.Enumerations;
using dotSpace.Objects.Network;
using Lifeforms;
using System;

namespace LifeformsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RemoteSpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/lifeforms?KEEP", new EntityFactory());
            int width = Math.Min(80, Console.BufferWidth);
            int height = Math.Min(25, Console.BufferHeight);
            Game lifeforms = new Game(width, height, remotespace);
            lifeforms.AddLifeform(3);
            lifeforms.AddLifeform(5);
            lifeforms.AddLifeform(7);
            lifeforms.AddLifeform(11);
            lifeforms.AddLifeform(13);
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
