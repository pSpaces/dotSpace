using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Spaces;
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
                    SpaceRepository repository = new SpaceRepository();
                    repository.AddGate("tcp://127.0.0.1:123?CONN");
                    repository.AddSpace("fridge", new FifoSpace());
                    AgentBase alice = new Producer("Alice", repository.GetSpace("fridge"));
                    alice.Start();
                    return;
                }
                else if (args[0] == "consumer")
                {
                    ISpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/fridge?CONN");
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new FoodConsumer("Bob", remotespace));
                    agents.Add(new FoodConsumer("Charlie", remotespace));
                    agents.Add(new DrugConsumer("Dave", remotespace));
                    agents.ForEach(a => a.Start());
                    Console.Read();
                    return;
                }
            }
            Console.WriteLine("Please specify [producer|consumer]");
            Console.Read();
        }
    }
}
