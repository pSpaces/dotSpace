using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace dotSpace.Objects
{
    public sealed class Space : ISpace
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private readonly List<ITuple> elements;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Space()
        {
            this.elements = new List<ITuple>();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public ITuple Get(IPattern pattern)
        {
            return this.Get(pattern.Fields);
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
        public ITuple GetP(IPattern pattern)
        {
            return this.GetP(pattern.Fields);
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
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.GetAll(pattern.Fields);
        }
        public IEnumerable<ITuple> GetAll(params object[] values)
        {
            IEnumerable<ITuple> t = null;
            lock (this.elements)
            {
                t = this.FindAll(values);
                t.Apply(x => this.elements.Remove(x));
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public ITuple Query(IPattern pattern)
        {
            return this.Query(pattern.Fields);
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
        public ITuple QueryP(IPattern pattern)
        {
            return this.QueryP(pattern.Fields);
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
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.QueryAll(pattern.Fields);
        }
        public IEnumerable<ITuple> QueryAll(params object[] values)
        {
            IEnumerable<ITuple> t = null;
            lock (this.elements)
            {
                t = this.FindAll(values);
                Monitor.PulseAll(this.elements);
            }
            return t;
        }
        public void Put(ITuple t)
        {
            this.Put(t.Fields);
        }
        public void Put(params object[] tuple)
        {
            lock (this.elements)
            {
                this.elements.Add(new Tuple(tuple));
                Monitor.PulseAll(this.elements);
            }
        }
        public bool Replace(IPattern pattern, params object[] values)
        {
            return this.Replace(pattern.Fields.Concat(values).ToArray());
        }
        public bool Replace(IPattern pattern, IPattern values)
        {
            return this.Replace(pattern.Fields.Concat(values.Fields).ToArray());
        }
        public bool Replace(params object[] values)
        {
            if (values.Length % 2 != 0)
            {
                throw new Exception("Cannot specify uneven number of pattern/value arguments");
            }
            object[] pattern = values.Take(values.Length / 2).ToArray();
            object[] newValues = values.Skip(values.Length / 2).Take(values.Length / 2).ToArray();
            ITuple t = null;
            lock (this.elements)
            {
                t = this.Find(pattern);
                if (t != null)
                {
                    Enumerable.Range(0, t.Size).Apply(x => t[x] = newValues[x] ?? t[x]);
                }
                Monitor.PulseAll(this.elements);
                return t == null;
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

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
        private IEnumerable<ITuple> FindAll(object[] pattern)
        {
            return this.elements.Where(x => this.Match(pattern, x.Fields)).ToList();
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
                if (pattern[idx] is Type)
                {
                    result &= tuple[idx] is Type ? tuple[idx].GetType() == (Type)pattern[idx] : tuple[idx].GetType() == (Type)pattern[idx];
                }
                else
                {
                    result &= tuple[idx].Equals(pattern[idx]);
                }
                if (!result) return false;
            }

            return result;
        } 

        #endregion
    }
}
