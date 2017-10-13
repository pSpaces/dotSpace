using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
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
                Console.WriteLine("Please specify your name");
                return;
            }
            // Instantiate a new Space repository.
            SpaceRepository repository = new SpaceRepository();
            
            // Add a gate, such that we can connect to it.
            repository.AddGate("tcp://127.0.0.1:123?CONN");

            // Add a new Fifo based space.
            repository.AddSpace("dtu", new SequentialSpace());

            // Insert a tuple with a message.
            repository.Put("dtu", "Hello student!");

            // Instantiate a remotespace (a networked space) thereby connecting to the spacerepository.
            ISpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/dtu?CONN");

            // Instantiate a new agent, assign the tuple space and start it.
            AgentBase student = new Student(args[0], remotespace);
            student.Start();

            // Wait and retrieve the message from the agent.
            ITuple tuple = repository.Get("dtu",typeof(string), typeof(string));

            // Print the contents to the console.
            Console.WriteLine(string.Format("{0}, you are attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }
    }
}
