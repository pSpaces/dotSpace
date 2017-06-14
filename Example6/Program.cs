using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Spaces;
using System;
using System.Collections.Generic;

namespace Example6
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "table")
                {
                    SpaceRepository repository = new SpaceRepository();
                    repository.AddGate("tcp://127.0.0.1:123?CONN");
                    repository.AddSpace("DiningTable", new FifoSpace());
                    repository.Put("DiningTable", "FORK", 1);
                    repository.Put("DiningTable", "FORK", 2);
                    repository.Put("DiningTable", "FORK", 3);
                    repository.Put("DiningTable", "FORK", 4);
                    repository.Put("DiningTable", "FORK", 5);
                    return;
                }
                else if (args[0] == "philosopher")
                {
                    ISpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/DiningTable?CONN");
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new Philosopher("Alice", 1, 5, remotespace));
                    agents.Add(new Philosopher("Charlie", 2, 5, remotespace));
                    agents.Add(new Philosopher("Bob", 3, 5, remotespace));
                    agents.Add(new Philosopher("Dave", 4, 5, remotespace));
                    agents.Add(new Philosopher("Homer", 5, 5, remotespace));
                    agents.ForEach(a => a.Start());
                    Console.Read();
                    return;
                }
            }
            Console.WriteLine("Please specify [table|philosopher]");
            Console.Read();

        }
    }
}
