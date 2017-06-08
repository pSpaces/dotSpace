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
            Node node = new Node(ConnectionMode.CONN, 123, "127.0.0.1");
            node.AddSpace("dtu", new Space());
            node.Put("dtu", "Hello world!");

            Target target = new Target(ConnectionMode.CONN, "127.0.0.1", 123);
            AgentBase student = new Student("sxxxxxx", target.GetRemoteSpace("dtu"));
            student.Start();
            ITuple tuple = node.Get("dtu",typeof(string), typeof(string));
            Console.WriteLine(string.Format("{0} is attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }
    }
}
