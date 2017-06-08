using dotSpace.Interfaces;
using dotSpace.Objects;
using System;

namespace LocalLifeforms
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpace ts = new Space();
            int width = 110;
            int height = 25;
            Game lifeforms = new Game(width, height, ts);
            lifeforms.AddLifeform(3, 100, 25);
            lifeforms.AddLifeform(5, 150, 25);
            lifeforms.AddLifeform(7, 250, 25);
            lifeforms.AddLifeform(11, 250, 25);
            lifeforms.AddLifeform(13, 250, 25);
            lifeforms.AddLifeform(17, 50, 25);
            lifeforms.AddLifeform(19, 175, 25);
            lifeforms.AddLifeform(21, 225, 25);
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
