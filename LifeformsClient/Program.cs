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
            TerminalInfo.Initialize(80, 24);
            Game lifeforms = new Game(remotespace);
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
