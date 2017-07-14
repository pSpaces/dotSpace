using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using Lifeforms;
using System.Threading;

namespace Test
{
    /// <summary>
    /// This class creates new offspring.
    /// </summary>
    public class LifeformDispatcher : AgentBase
    {
        protected int width;
        protected int height;

        public LifeformDispatcher(ISpace ts) : base("dispatcher", ts)
        {
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
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
