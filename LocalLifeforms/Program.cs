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
            int width = 80;
            int height = 25;
            Game lifeforms = new Game(width, height, ts);
            lifeforms.AddLifeform(3, 100, 25);
            lifeforms.AddLifeform(5, 150, 35);
            lifeforms.AddLifeform(7, 250, 55);
            lifeforms.AddLifeform(11, 250, 55);
            lifeforms.AddLifeform(13, 250, 55);
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
