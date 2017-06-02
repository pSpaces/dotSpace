using dotSpace.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace dotSpace.Objects
{
    public class TupleSpace : ITupleSpace
    {
        private readonly List<ITuple> elements;
        public TupleSpace()
        {
            this.elements = new List<ITuple>();
        }

        public ITuple Get(IPattern pattern)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.WaitUntilMatch(pattern);
                this.elements.Remove(t);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public ITuple GetP(IPattern pattern)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.Find(pattern);
                this.elements.Remove(t);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public ITuple Query(IPattern pattern)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.WaitUntilMatch(pattern);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public ITuple QueryP(IPattern pattern)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.Find(pattern);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public void Put(ITuple t)
        {
            lock (this.elements)
            {
                this.elements.Add(t);
                Monitor.PulseAll(this.elements);
            }
        }

        private ITuple WaitUntilMatch(IPattern pattern)
        {
            ITuple t;
            while (((t = this.Find(pattern)) == null))
            {
                Monitor.PulseAll(this.elements);
                Monitor.Wait(this.elements);
            }
            return t;
        }
        private ITuple Find(IPattern pattern)
        {
            return this.elements.Where(x => pattern.Match(x)).FirstOrDefault();
        }

    }
}
