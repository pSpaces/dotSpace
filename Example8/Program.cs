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
                    Node node = new Node(ConnectionMode.CONN, 123, "127.0.0.1");
                    node.AddSpace("fridge", new Space());
                    AgentBase alice = new Alice("Alice", node["fridge"]);
                    alice.Start();
                    return;
                }
                else if (arg == "b+c")
                {
                    Target target = new Target(ConnectionMode.CONN, "127.0.0.1", 123);
                    AgentBase bob = new Bob("Bob", target.GetRemoteSpace("fridge"));
                    AgentBase charlie = new Charlie("Charlie", target.GetRemoteSpace("fridge"));
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
