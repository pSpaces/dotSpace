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
            ServerNode server = new ServerNode(ConnectionMode.CONN, 123, "127.0.0.1");
            server.AddSpace("dtu", new Space());
            server.Put("dtu", "Hello world!");

            ClientNode client = new ClientNode(ConnectionMode.CONN, "127.0.0.1", 123);
            AgentBase student = new Student("sxxxxxx", client.GetRemoteSpace("dtu"));
            student.Start();
            ITuple tuple = server.Get("dtu",typeof(string), typeof(string));
            Console.WriteLine(string.Format("{0} is attending course {1}", tuple[0], tuple[1]));
            Console.Read();
        }
    }
}
