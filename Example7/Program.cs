using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;
using System.Collections.Generic;

namespace Example7
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "producer")
                {
                    ServerNode server = new ServerNode(ConnectionMode.CONN, 123, "127.0.0.1");
                    server.AddSpace("fridge", new Space());
                    AgentBase alice = new Producer("Alice", server["fridge"]);
                    alice.Start();
                    return;
                }
                else if (args[0] == "consumer")
                {
                    ClientNode client = new ClientNode(ConnectionMode.CONN, "127.0.0.1", 123);
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new FoodConsumer("Bob", client.GetRemoteSpace("fridge")));
                    agents.Add(new FoodConsumer("Charlie", client.GetRemoteSpace("fridge")));
                    agents.Add(new DrugConsumer("Dave", client.GetRemoteSpace("fridge")));
                    agents.ForEach(a => a.Start());
                    return;
                }
            }
            Console.WriteLine("Please specify [producer|consumer]");
            Console.Read();
        }
    }
}
