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
                    ServerNode server = new ServerNode(ConnectionMode.CONN, 123, "127.0.0.1");
                    server.AddSpace("fridge", new Space());
                    AgentBase alice = new Alice("Alice", server["fridge"]);
                    alice.Start();
                    return;
                }
                else if (arg == "b+c")
                {
                    ClientNode client = new ClientNode(ConnectionMode.CONN, "127.0.0.1", 123);
                    AgentBase bob = new Bob("Bob", client.GetRemoteSpace("fridge"));
                    AgentBase charlie = new Charlie("Charlie", client.GetRemoteSpace("fridge"));
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
