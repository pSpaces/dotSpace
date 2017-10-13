using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using Lifeforms;
using System;

namespace LocalLifeforms
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpace ts = new SequentialSpace(new EntityFactory());
            TerminalInfo.Initialize(80, 24);
            Game lifeforms = new Game(ts);
            lifeforms.AddLifeform(3, 100, 25);
            lifeforms.AddLifeform(5, 150, 25);
            lifeforms.AddLifeform(7, 90, 25);
            lifeforms.AddLifeform(11, 200, 25);
            lifeforms.AddLifeform(13, 130, 25);
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
