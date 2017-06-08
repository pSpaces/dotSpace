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
            this.ts.Query("start");
            // Keep iterating while the state is 'running'
            while (this.ts.QueryP("running", true) != null)
            {
                // <spawn, genom, p1genom, p2genom, life, food, x, y>
                ITuple spawningLifeform = this.ts.Get("spawn", typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
                long genom = (long)spawningLifeform[1];
                long p1genom = (long)spawningLifeform[2];
                long p2genom = (long)spawningLifeform[3];
                int life = (int)spawningLifeform[4];
                int food = (int)spawningLifeform[5];
                int x = (int)spawningLifeform[6];
                int y = (int)spawningLifeform[7];
                int generation = (int)spawningLifeform[8];
                Lifeform lifeform = new Lifeform(genom, p1genom, p2genom, life, food, x, y, generation, this.width, this.height, this.ts);
                lifeform.Start();
                Thread.Sleep(100);
            }
        }
    }
}
