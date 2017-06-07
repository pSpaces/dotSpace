using dotSpace.BaseClasses;
using dotSpace.Objects;
using System.Collections.Generic;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            Space ts = new Space();
            List<AgentBase> agents = new List<AgentBase>();

            // We create Alice and Bob as Producer/Consumer agents
            // The constructor of agents takes the name of the agent as argument
            agents.Add(new Producer("Alice", ts));
            agents.Add(new FoodConsumer("Bob", ts));
            agents.Add(new FoodConsumer("Charlie", ts));
            agents.Add(new DrugConsumer("Dave", ts));
            // We start the agents
            agents.ForEach(a => a.Start());
        }
    }
}
