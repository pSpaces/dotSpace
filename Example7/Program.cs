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
                    SpaceRepository repository = new SpaceRepository(ConnectionMode.CONN, 123, "127.0.0.1");
                    repository.AddSpace("fridge", new Space());
                    AgentBase alice = new Producer("Alice", repository["fridge"]);
                    alice.Start();
                    return;
                }
                else if (args[0] == "consumer")
                {
                    Gate gate = new Gate(ConnectionMode.CONN, "127.0.0.1", 123);
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new FoodConsumer("Bob", gate.GetSpace("fridge")));
                    agents.Add(new FoodConsumer("Charlie", gate.GetSpace("fridge")));
                    agents.Add(new DrugConsumer("Dave", gate.GetSpace("fridge")));
                    agents.ForEach(a => a.Start());
                    return;
                }
            }
            Console.WriteLine("Please specify [producer|consumer]");
            Console.Read();
        }
    }
}
