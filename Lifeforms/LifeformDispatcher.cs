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
            this.Query("start");
            // Keep iterating while the state is 'running'
            while (this.QueryP("running", true) != null)
            {
                // <spawn, genom, p1genom, p2genom, life, food, x, y>
                ITuple spawningLifeform = this.Get("spawn", typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
                long genom = (long)spawningLifeform[1];
                long p1genom = (long)spawningLifeform[2];
                long p2genom = (long)spawningLifeform[3];
                int life = (int)spawningLifeform[4];
                int food = (int)spawningLifeform[5];
                int x = (int)spawningLifeform[6];
                int y = (int)spawningLifeform[7];
                int generation = (int)spawningLifeform[8];
                int visualRange = (int)spawningLifeform[9];
                int maxNrChildren  = (int)spawningLifeform[10];
                int speed = (int)spawningLifeform[11];
                Lifeform lifeform = new Lifeform(genom, p1genom, p2genom, life, food, x, y, generation, visualRange, maxNrChildren, speed, this.width, this.height, this.space);
                lifeform.Start();
                Thread.Sleep(100);
            }
        }
    }
}
