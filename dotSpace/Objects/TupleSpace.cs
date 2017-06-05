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
            return this.Get(pattern.Fields);
        }
        public ITuple GetP(IPattern pattern)
        {
            return this.GetP(pattern.Fields);
        }
        public ITuple Query(IPattern pattern)
        {
            return this.Query(pattern.Fields);
        }
        public ITuple QueryP(IPattern pattern)
        {
            return this.QueryP(pattern.Fields);
        }
        public void Put(ITuple t)
        {
            this.Put(t.Fields);
        }


        public ITuple Get(params object[] values)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.WaitUntilMatch(values);
                this.elements.Remove(t);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public ITuple GetP(params object[] pattern)
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
        public ITuple Query(params object[] pattern)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.WaitUntilMatch(pattern);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public ITuple QueryP(params object[] pattern)
        {
            ITuple t = null;
            lock (this.elements)
            {
                t = this.Find(pattern);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public void Put(params object[] tuple)
        {
            lock (this.elements)
            {
                this.elements.Add(new Tuple(tuple));
                Monitor.PulseAll(this.elements);
            }
        }

        private ITuple WaitUntilMatch(object[] pattern)
        {
            ITuple t;
            while (((t = this.Find(pattern)) == null))
            {
                Monitor.PulseAll(this.elements);
                Monitor.Wait(this.elements);
            }
            return t;
        }
        private ITuple Find(object[] pattern)
        {
            return this.elements.Where(x => this.Match(pattern, x.Fields)).FirstOrDefault();
        }
        private bool Match(object[] pattern, object[] tuple)
        {
            if (tuple.Length != pattern.Length)
            {
                return false;
            }
            bool result = true;
            for (int idx = 0; idx < tuple.Length; idx++)
            {
                if (pattern[idx] != null)
                {
                    result &= tuple[idx].Equals(pattern[idx]);
                    if (!result) return false;
                }
            }

            return result;
        }

    }
}
