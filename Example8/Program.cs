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
                    SpaceRepository repository = new SpaceRepository();
                    repository.AddGate("tcp://127.0.0.1:123?CONN");
                    repository.AddSpace("fridge", new Space());
                    AgentBase alice = new Alice("Alice", repository.GetSpace("fridge"));
                    alice.Start();
                    return;
                }
                else if (arg == "b+c")
                {
                    RemoteSpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/fridge?CONN");
                    AgentBase bob = new Bob("Bob", remotespace);
                    AgentBase charlie = new Charlie("Charlie", remotespace);
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
