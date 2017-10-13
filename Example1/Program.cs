using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using dotSpace.Objects.Space;
using System;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length!=1)
            {
                Console.WriteLine("Please specify your name");
                return;
            }
            // Instantiate a new Fifobased tuple space.
            ISpace dtu = new SequentialSpace();
            
            // Insert a tuple with a message.
            dtu.Put("Hello student!");
            
            // Instantiate a new agent, assign the tuple space and start it.
            AgentBase student = new Student(args[0], dtu);
            student.Start();

            // Wait and retrieve the message from the agent.
            ITuple tuple = dtu.Get(typeof(string), typeof(string));

            // Print the contents to the console.
            Console.WriteLine(string.Format("{0}, you are attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }
    }
}
