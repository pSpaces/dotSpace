using dotSpace.Objects.Network;
using dotSpace.Objects.Space;
using System;

namespace Example7
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Instantiate a new space repository, and add two seperate gates.
                SpaceRepository repository = new SpaceRepository();
                repository.AddGate("tcp://127.0.0.1:123?KEEP");
                repository.AddGate("tcp://127.0.0.1:124?KEEP");

                // Create a new fifo based space, and insert the ping.
                repository.AddSpace("pingpong", new SequentialSpace());
                repository.Put("pingpong", "ping", 0);

                // Create two seperate remotespaces and agents.
                // The agents use their own private remotespace.
                RemoteSpace remotespace1 = new RemoteSpace("tcp://127.0.0.1:123/pingpong?KEEP");
                PingPong a1 = new PingPong("ping", "pong", remotespace1);

                RemoteSpace remotespace2 = new RemoteSpace("tcp://127.0.0.1:124/pingpong?KEEP");               
                PingPong a2 = new PingPong("pong", "ping", remotespace2);

                // Start the agents
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
