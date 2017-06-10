using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Objects;
using dotSpace.Objects.Network;
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
                    SpaceRepository repository = new SpaceRepository(ConnectionMode.CONN, 123, "127.0.0.1");
                    repository.AddSpace("DiningTable", new Space());
                    repository.Put("DiningTable", "FORK", 1);
                    repository.Put("DiningTable", "FORK", 2);
                    repository.Put("DiningTable", "FORK", 3);
                    repository.Put("DiningTable", "FORK", 4);
                    repository.Put("DiningTable", "FORK", 5);
                    return;
                }
                else if (args[0] == "philosopher")
                {
                    Gate gate = new Gate(ConnectionMode.CONN, "127.0.0.1", 123);
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new Philosopher("Alice", 1, 5, gate.GetSpace("DiningTable")));
                    agents.Add(new Philosopher("Charlie", 2, 5, gate.GetSpace("DiningTable")));
                    agents.Add(new Philosopher("Bob", 3, 5, gate.GetSpace("DiningTable")));
                    agents.Add(new Philosopher("Dave", 4, 5, gate.GetSpace("DiningTable")));
                    agents.Add(new Philosopher("Homer", 5, 5, gate.GetSpace("DiningTable")));
                    agents.ForEach(a => a.Start());
                    return;
                }
            }
            Console.WriteLine("Please specify [table|philosopher]");
            Console.Read();

        }
    }
}
