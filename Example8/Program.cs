using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Spaces;
using System;

namespace Example8
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string arg = args[0].ToString();
                if (arg == "alice")
                {
                    SpaceRepository repository = new SpaceRepository();
                    repository.AddGate("tcp://127.0.0.1:123?CONN");
                    repository.AddSpace("fridge", new FifoSpace());
                    AgentBase alice = new Alice("Alice", repository.GetSpace("fridge"));
                    alice.Start();
                    return;
                }
                else if (arg == "b+c")
                {
                    ISpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/fridge?CONN");
                    AgentBase bob = new Bob("Bob", remotespace);
                    AgentBase charlie = new Charlie("Charlie", remotespace);
                    bob.Start();
                    charlie.Start();
                    Console.Read();
                    return;
                }
            }
            Console.WriteLine("Please specify [Alice|B+C]");
            Console.Read();

        }
    }
}
