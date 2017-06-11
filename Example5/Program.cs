using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects;
using dotSpace.Objects.Network;
using System;

namespace Example5
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceRepository repository = new SpaceRepository();
            repository.AddGate("tcp://127.0.0.1:123?CONN");
            repository.AddSpace("dtu", new Space());
            repository.Put("dtu", "Hello world!");

            RemoteSpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/dtu?CONN");
            AgentBase student = new Student("sxxxxxx", remotespace);
            student.Start();
            ITuple tuple = repository.Get("dtu",typeof(string), typeof(string));
            Console.WriteLine(string.Format("{0} is attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }
    }
}
