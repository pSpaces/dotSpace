using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using System;
using System.Collections.Generic;

namespace Example2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate a new Fifo based space.
            ISpace ts = new SequentialSpace();

            // Insert the forks that the philosophers must share.
            ts.Put("FORK", 1);
            ts.Put("FORK", 2);
            ts.Put("FORK", 3);
            ts.Put("FORK", 4);
            ts.Put("FORK", 5);

            // Instantiate the philosopher agents.
            List<AgentBase> agents = new List<AgentBase>();
            agents.Add(new Philosopher("Alice", 1, 5, ts));
            agents.Add(new Philosopher("Charlie", 2, 5, ts));
            agents.Add(new Philosopher("Bob", 3, 5, ts));
            agents.Add(new Philosopher("Dave", 4, 5, ts));
            agents.Add(new Philosopher("Homer", 5, 5, ts));
            
            // Let the philosophers eat.
            agents.ForEach(a => a.Start());
            Console.Read();
        }
    }
}
