using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;
using System.Collections.Generic;

namespace Example8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string arg = args[0].ToString();
                if (arg == "alice")
                {
                    SpaceRepository repository = new SpaceRepository(ConnectionMode.CONN, 123, "127.0.0.1");
                    repository.AddSpace("fridge", new Space());
                    AgentBase alice = new Alice("Alice", repository.GetSpace("fridge"));
                    alice.Start();
                    return;
                }
                else if (arg == "b+c")
                {
                    Gate gate = new Gate(ConnectionMode.CONN, "127.0.0.1", 123);
                    AgentBase bob = new Bob("Bob", gate.GetSpace("fridge"));
                    AgentBase charlie = new Charlie("Charlie", gate.GetSpace("fridge"));
                    bob.Start();
                    charlie.Start();
                    return;
                }
            }
            Console.WriteLine("Please specify [Alice|B+C]");
            Console.Read();

        }
    }
}
