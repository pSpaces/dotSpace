using dotSpace.Enumerations;
using dotSpace.Objects.Network;
using System;

namespace LifeformsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Gate gate = new Gate(ConnectionMode.CONN, "127.0.0.1", 123);
            int width = 80;
            int height = 25;
            Game lifeforms = new Game(width, height, gate.GetSpace("lifeforms"));
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
