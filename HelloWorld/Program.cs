using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            SequentialSpace dtu = new SequentialSpace();
            dtu.Put("Hello world!");
            ITuple tuple = dtu.Get(typeof(string));
            Console.WriteLine(tuple);
            Console.Read();
        }
    }
}
