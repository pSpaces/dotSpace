using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects;
using System;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            Space dtu = new Space();
            dtu.Put("Hello world!");
            AgentBase student = new Student("sxxxxxx", dtu);
            student.Start();
            ITuple tuple = dtu.Get(typeof(string), typeof(string));
            Console.WriteLine(string.Format("{0} is attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }

    }
}
