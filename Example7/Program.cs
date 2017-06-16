using dotSpace.Objects.Network;
using dotSpace.Objects.Spaces;
using System;

namespace Example7
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SpaceRepository repository = new SpaceRepository();
                repository.AddGate("tcp://127.0.0.1:123?KEEP");
                repository.AddGate("tcp://127.0.0.1:124?KEEP");
                repository.AddSpace("pingpong", new RandomSpace());
                repository.Put("pingpong", "ping", 0);

                RemoteSpace remotespace1 = new RemoteSpace("tcp://127.0.0.1:123/pingpong?KEEP");
                PingPong a1 = new PingPong("ping", "pong", remotespace1);

                RemoteSpace remotespace2 = new RemoteSpace("tcp://127.0.0.1:124/pingpong?KEEP");
                PingPong a2 = new PingPong("pong", "ping", remotespace2);
                a1.Start();
                a2.Start();
                Console.Read();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
