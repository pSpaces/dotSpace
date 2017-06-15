using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using dotSpace.Objects.Spaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace Test
{


    class Program
    {


        static void TestAll()
        {
            SpaceRepository repository = new SpaceRepository();
            repository.AddGate("tcp://127.0.0.1:123?CONN");
            repository.AddSpace("myspace", new FifoSpace());
            RemoteSpace remotespace = new RemoteSpace("tcp://127.0.0.1:123/myspace?CONN");

            repository.Put("myspace", "hej", 1213, "alberto");
            repository.Put("myspace", "working with paramstyle tuples", 1213, "alberto");
            repository.GetSpace("myspace")?.Query("hej", 1213, "alberto");

            // client working on the remote tuplespace.
            remotespace.Put("myspace", "hej", 1213, "shane");
            remotespace.Put("myspace", new dotSpace.Objects.Spaces.Tuple("hej", 1213, "shane"));

            IEnumerable<ITuple> tuples1 = remotespace.QueryAll("myspace", typeof(string), typeof(int), typeof(string));

            ITuple tuple0 = remotespace.QueryP("myspace", new Pattern(typeof(string), 1213, "shane"));
            tuple0 = remotespace.QueryP("myspace", typeof(string), 1213, "shane");

            ITuple tuple1 = remotespace.Query("myspace", new Pattern(typeof(string), 1213, "shane"));
            tuple1 = remotespace.Query("myspace", typeof(string), 1213, "shane");

            ITuple tuple2 = remotespace.Get("myspace", new Pattern(typeof(string), 1213, "shane"));
            tuple2 = remotespace.Get("myspace", typeof(string), 1213, "shane");

            ITuple tuple3 = remotespace.GetP("myspace", new Pattern(typeof(string), 1213, "shane"));
            tuple3 = remotespace.GetP("myspace", typeof(string), 1213, "shane");

            ITuple tuple4 = remotespace.QueryP("myspace", new Pattern(typeof(string), 1213, "shane"));
            tuple4 = remotespace.QueryP("myspace", typeof(string), 1213, "shane");

            ITuple tuple5 = remotespace.Get("myspace", new Pattern(typeof(string), 1213, "alberto"));

            tuple5 = remotespace.Get("myspace", typeof(string), 1213, "alberto");
            IEnumerable<ITuple> tuples2 = remotespace.GetAll("myspace", typeof(string), typeof(int), typeof(string));
        }


        static void udptest()
        {
            // Create UDP client
            int receiverPort = 20000;
            UdpClient receiver = new UdpClient(receiverPort); /// hvor vi lytter

            // Display some information
            Console.WriteLine("Starting Upd receiving on port: " + receiverPort);
            Console.WriteLine("Press any key to quit.");
            Console.WriteLine("-------------------------------\n");


            //// Start async receiving
            //receiver.BeginReceive(DataReceived, receiver);

            // Send some test messages
            using (UdpClient sender1 = new UdpClient(19999)) // hvor vi lytter
                sender1.Send(Encoding.ASCII.GetBytes("Hi!"), 3, "localhost", receiverPort); // hvor det skal hen
            using (UdpClient sender2 = new UdpClient(20001))
                sender2.Send(Encoding.ASCII.GetBytes("Hi!"), 3, "localhost", receiverPort);

            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0); // modtagerens info
            Byte[] receivedBytes = receiver.Receive(ref receivedIpEndPoint);
            // Convert data to ASCII and print in console
            string receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);
            Console.Write("BLAH:" +receivedIpEndPoint + ": " + receivedText + Environment.NewLine);
            // Wait for any key to terminate application
            Console.ReadKey();

        }
        private static void DataReceived(IAsyncResult ar)
        {
            UdpClient c = (UdpClient)ar.AsyncState;
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Byte[] receivedBytes = c.EndReceive(ar, ref receivedIpEndPoint);

            // Convert data to ASCII and print in console
            string receivedText = ASCIIEncoding.ASCII.GetString(receivedBytes);
            Console.Write(receivedIpEndPoint + ": " + receivedText + Environment.NewLine);

            // Restart listening for udp data packages
            c.BeginReceive(DataReceived, ar.AsyncState);
        }

        static void Main(string[] args)
        {
            try
            {

                SpaceRepository repository = new SpaceRepository();
                repository.AddGate("tcp://127.0.0.1:123?KEEP");
                repository.AddGate("tcp://127.0.0.1:124?KEEP");
                repository.AddSpace("pingpong", new RandomSpace());
                repository.Put("pingpong", "ping", "value", 0);
                RemoteSpace remotespace1 = new RemoteSpace("tcp://127.0.0.1:123/pingpong?KEEP");
                RemoteSpace remotespace2 = new RemoteSpace("tcp://127.0.0.1:124/pingpong?KEEP");
                ISpace space = new FifoSpace();
                space.Put("agent1", "value", 0);
                GateAgent a1 = new GateAgent("ping", "pong", remotespace1);
                GateAgent a2 = new GateAgent("pong", "ping", remotespace2);
                a1.Start();
                a2.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public class GateAgent : AgentBase
        {
            private string other;
            public GateAgent(string name, string other, ISpace ts) : base(name, ts)
            {
                this.other = other;
            }
            protected override void DoWork()
            {
                try
                {
                    while (true)
                    {
                        ITuple t = this.Get(this.other, "value", typeof(int));
                        int value = (int)t[2] + 1;
                        t[1] = value;
                        Console.WriteLine(string.Format("{0}: {1}", this.name, value));
                        this.Put(this.name, "value", value);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

    }
}
