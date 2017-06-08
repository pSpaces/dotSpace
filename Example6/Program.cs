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
                    ServerNode server = new ServerNode(ConnectionMode.CONN, 123, "127.0.0.1");
                    server.AddSpace("DiningTable", new Space());
                    server.Put("DiningTable", "FORK", 1);
                    server.Put("DiningTable", "FORK", 2);
                    server.Put("DiningTable", "FORK", 3);
                    server.Put("DiningTable", "FORK", 4);
                    server.Put("DiningTable", "FORK", 5);
                    return;
                }
                else if (args[0] == "philosopher")
                {
                    ClientNode client = new ClientNode(ConnectionMode.CONN, "127.0.0.1", 123);
                    List<AgentBase> agents = new List<AgentBase>();
                    agents.Add(new Philosopher("Alice", 1, 5, client.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Charlie", 2, 5, client.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Bob", 3, 5, client.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Dave", 4, 5, client.GetRemoteSpace("DiningTable")));
                    agents.Add(new Philosopher("Homer", 5, 5, client.GetRemoteSpace("DiningTable")));
                    agents.ForEach(a => a.Start());
                    return;
                }
            }
            Console.WriteLine("Please specify [table|philosopher]");
            Console.Read();

        }
    }
}
