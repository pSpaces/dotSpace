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
            lifeforms.AddLifeform(7, 90, 25);
            lifeforms.AddLifeform(11, 200, 25);
            lifeforms.AddLifeform(13, 130, 25);
            lifeforms.AddLifeform(17, 50, 25);
            lifeforms.AddLifeform(19, 175, 25);
            lifeforms.AddLifeform(21, 125, 25);
            lifeforms.AddLifeform(23, 75, 25);
            lifeforms.AddLifeform(27, 175, 25);
            lifeforms.AddLifeform(31, 275, 25);
            lifeforms.Run();
            Console.ReadKey();
            lifeforms.Stop();
        }
    }
}
