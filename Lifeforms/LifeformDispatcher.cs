using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using Lifeforms;
using System.Threading;

namespace Test
{
    public class LifeformDispatcher : AgentBase
    {
        protected int width;
        protected int height;

        public LifeformDispatcher(int width, int height, ISpace ts) : base("dispatcher", ts)
        {
            this.width = width;
            this.height = height;
        }


        protected override void DoWork()
        {
            // Wait until we can start
            this.Query(EntityType.SIGNAL, "start");
            // Keep iterating while the state is 'running'
            while (this.QueryP(EntityType.SIGNAL, "running", true) != null)
            {
                // <spawn, genom, p1genom, p2genom, life, food, x, y>
                SpawnLifeform spawn = (SpawnLifeform)this.Get(EntityType.SPAWN, typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
                Lifeform lifeform = new Lifeform(spawn, this.width, this.height, this.space);
                lifeform.Start();
                Thread.Sleep(100);
            }
        }
    }
}
