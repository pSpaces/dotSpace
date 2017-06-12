using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using dotSpace.Objects.Spaces;
using System.Collections.Generic;

namespace Example4
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpace ts = new FifoSpace();
            List<AgentBase> agents = new List<AgentBase>();

            // We create Alice and Bob as Producer/Consumer agents
            // The constructor of agents takes the name of the agent as argument
            agents.Add(new Alice("Alice", ts));
            agents.Add(new Bob("Bob", ts));
            agents.Add(new Charlie("Charlie", ts));
            // We start the agents
            agents.ForEach(a => a.Start());
        }
    }
}
