using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Space;
using System;

namespace Example5
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify your student number");
            }
            SpaceRepository repository = new SpaceRepository();
            repository.AddGate("tcp://127.0.0.1:123?CONN");
            repository.AddSpace("dtu", new FifoSpace());
            repository.Put("dtu", "Hello student!");

            ISpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/dtu?CONN");
            AgentBase student = new Student(args[0], remotespace);
            student.Start();
            ITuple tuple = repository.Get("dtu",typeof(string), typeof(string));
            Console.WriteLine(string.Format("{0} is attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }
    }
}
