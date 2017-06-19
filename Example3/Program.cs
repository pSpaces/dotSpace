using dotSpace.Interfaces;
using dotSpace.Objects.Space;
using System;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpace pingpongTable = new FifoSpace();
            pingpongTable.Put("ping", 0);
            PingPong a1 = new PingPong("ping", "pong", pingpongTable);
            PingPong a2 = new PingPong("pong", "ping", pingpongTable);
            a1.Start();
            a2.Start();
            Console.Read();
        }
    }
}
