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
                    Node node = new Node(ConnectionMode.CONN, 123, "127.0.0.1");
                    node.AddSpace("DiningTable", new Space());
                    node.Put("DiningTable", "FORK", 1);
                    node.Put("DiningTable", "FORK", 2);
                    node.Put("DiningTable", "FORK", 3);
                    node.Put("DiningTable", "FORK", 4);
                    node.Put("DiningTable", "FORK", 5);
                    return;
                }
                else if (args[0] == "philosopher")
                {
                    Target target = new Target(ConnectionMode.CONN, "127.0.0.1", 123);
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new Philosopher("Alice", 1, 5, target.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Charlie", 2, 5, target.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Bob", 3, 5, target.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Dave", 4, 5, target.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Homer", 5, 5, target.GetRemoteSpace("DiningTable")));
                    agents.ForEach(a => a.Start());
                    return;
                }
            }
            Console.WriteLine("Please specify [table|philosopher]");
            Console.Read();

        }
    }
}
