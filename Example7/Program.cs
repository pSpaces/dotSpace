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
                    Node node = new Node(ConnectionMode.CONN, 123, "127.0.0.1");
                    node.AddSpace("fridge", new Space());
                    AgentBase alice = new Producer("Alice", node["fridge"]);
                    alice.Start();
                    return;
                }
                else if (args[0] == "consumer")
                {
                    Target target = new Target(ConnectionMode.CONN, "127.0.0.1", 123);
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new FoodConsumer("Bob", target.GetRemoteSpace("fridge")));
                    agents.Add(new FoodConsumer("Charlie", target.GetRemoteSpace("fridge")));
                    agents.Add(new DrugConsumer("Dave", target.GetRemoteSpace("fridge")));
                    agents.ForEach(a => a.Start());
                    return;
                }
            }
            Console.WriteLine("Please specify [producer|consumer]");
            Console.Read();
        }
    }
}
