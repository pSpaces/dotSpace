using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using System;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate a new Fifobased tuplespace.
            ISpace pingpongTable = new SequentialSpace();
            
            // Insert the ping.
            pingpongTable.Put("ping", 0);
            
            // Create two PingPong agents and start them.
            PingPong a1 = new PingPong("ping", "pong", pingpongTable);
            PingPong a2 = new PingPong("pong", "ping", pingpongTable);
            a1.Start();
            a2.Start();
            Console.Read();
        }
    }
}
