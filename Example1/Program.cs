using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Spaces;
using System;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length!=1)
            {
                Console.WriteLine("Please specify your student number");
            }
            ISpace dtu = new FifoSpace();
            dtu.Put("Hello student!");
            AgentBase student = new Student(args[0], dtu);
            student.Start();
            ITuple tuple = dtu.Get(typeof(string), typeof(string));
            Console.WriteLine(string.Format("{0} you are attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }

    }
}
