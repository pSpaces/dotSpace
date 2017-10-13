using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Network;
using dotSpace.Objects.Space;
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
                if (args[0] == "producer")
                {
                    // Create a new space repository, and add a new gate to it. 
                    // The gate is using CONN, meaning the connection is NOT persistent.
                    SpaceRepository repository = new SpaceRepository();
                    repository.AddGate("tcp://127.0.0.1:123?CONN");
                    
                    // Add a new fifo based space
                    repository.AddSpace("fridge", new SequentialSpace());
                    
                    // Create a new agent, and let the agent use the local tuple space instead of a networked remotespace.
                    AgentBase alice = new Producer("Alice", repository.GetSpace("fridge"));
                    alice.Start();
                    return;
                }
                else if (args[0] == "consumer")
                {
                    // The consumers use a remote space to access the space repository.
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
