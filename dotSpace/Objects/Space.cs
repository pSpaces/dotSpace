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

        private readonly Dictionary<ulong, List<ITuple>> buckets;
        private readonly Dictionary<ulong, ReaderWriterLockSlim> bucketLocks;
        private readonly object bucketAccess;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public Space()
        {
            this.buckets = new Dictionary<ulong, List<ITuple>>();
            this.bucketLocks = new Dictionary<ulong, ReaderWriterLockSlim>();
            this.bucketAccess = new object();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public ITuple Get(IPattern pattern)
        {
            return this.Get(pattern.Fields);
        }
        public ITuple Get(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            ITuple t = this.WaitUntilMatch(bucket, bucketLock, pattern);
            // Guard against duplication from retrieval
            bool successs = true;
            bucketLock.EnterWriteLock();
            successs = bucket.Remove(t);
            bucketLock.ExitWriteLock();
            return successs ? t : null;
        }
        public ITuple GetP(IPattern pattern)
        {
            return this.GetP(pattern.Fields);
        }
        public ITuple GetP(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            ITuple t = this.Find(bucket, bucketLock, pattern);
            // Guard against duplication from retrieval
            bool success = true;
            if (t != null)
            {
                bucketLock.EnterWriteLock();
                success = bucket.Remove(t);
                bucketLock.ExitWriteLock();
            }
            return success ? t : null;
        }
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.GetAll(pattern.Fields);
        }
        public IEnumerable<ITuple> GetAll(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            IEnumerable<ITuple> t = this.FindAll(bucket, bucketLock, pattern);

            if (t != null)
            {
                bucketLock.EnterWriteLock();
                t.Apply(x => bucket.Remove(x));
                bucketLock.ExitWriteLock();
            }
            return t;
        }
        public ITuple Query(IPattern pattern)
        {
            return this.Query(pattern.Fields);
        }
        public ITuple Query(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            return this.WaitUntilMatch(bucket, bucketLock, pattern);
        }
        public ITuple QueryP(IPattern pattern)
        {
            return this.QueryP(pattern.Fields);
        }
        public ITuple QueryP(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);
            return this.Find(bucket, bucketLock, pattern);
        }
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.QueryAll(pattern.Fields);
        }
        public IEnumerable<ITuple> QueryAll(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);
            return this.FindAll(bucket, bucketLock, pattern);
        }
        public void Put(ITuple t)
        {
            this.Put(t.Fields);
        }
        public void Put(params object[] values)
        {
            ulong hash = this.ComputeHash(values);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            bucketLock.EnterWriteLock();
            bucket.Add(new Tuple(values));
            bucketLock.ExitWriteLock();
            this.Awake(bucket);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private ulong ComputeHash(object[] values)
        {
            ulong result = 31;
            foreach (object value in values)
            {
                Type t = (value is Type) ? (Type)value : value.GetType();
                result = result * 31 + (ulong)t.GetHashCode();
            }
            return result;
        }
        private ITuple WaitUntilMatch(List<ITuple> bucket, ReaderWriterLockSlim bucketLock, object[] pattern)
        {
            ITuple t;
            while (((t = this.Find(bucket, bucketLock, pattern)) == null))
            {
                this.Wait(bucket);
            }
            return t;
        }
        private ITuple Find(List<ITuple> bucket, ReaderWriterLockSlim bucketLock, object[] pattern)
        {
            bucketLock.EnterReadLock();
            ITuple t = bucket.Where(x => this.Match(pattern, x.Fields)).FirstOrDefault();
            bucketLock.ExitReadLock();
            return t;
        }
        private IEnumerable<ITuple> FindAll(List<ITuple> bucket, ReaderWriterLockSlim bucketLock, object[] pattern)
        {
            bucketLock.EnterReadLock();
            IEnumerable<ITuple> t = bucket.Where(x => this.Match(pattern, x.Fields)).ToList();
            bucketLock.ExitReadLock();
            return t;
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
                    Type tupleType = tuple[idx] is Type ? (Type)tuple[idx] : tuple[idx].GetType();
                    result &= this.IsOfType(tupleType, (Type)pattern[idx]);
                }
                else
                {
                    result &= tuple[idx].Equals(pattern[idx]);
                }
                if (!result) return false;
            }

            return result;
        }
        private bool IsOfType(Type tupleType, Type patternType)
        {
            return tupleType == patternType;
        }
        private List<ITuple> GetBucket(ulong hash)
        {
            if (!this.buckets.ContainsKey(hash))
            {
                this.buckets.Add(hash, new List<ITuple>());
            }
            return this.buckets[hash];
        }
        private ReaderWriterLockSlim GetBucketLock(ulong hash)
        {
            if (!this.bucketLocks.ContainsKey(hash))
            {
                this.bucketLocks.Add(hash, new ReaderWriterLockSlim());
            }
            return this.bucketLocks[hash];
        }
        private void Wait(object _lock)
        {
            Monitor.Enter(_lock);
            Monitor.Wait(_lock);
            Monitor.Exit(_lock);
        }
        private void Awake(object _lock)
        {
            Monitor.Enter(_lock);
            Monitor.PulseAll(_lock);
            Monitor.Exit(_lock);
        }

        #endregion
    }
}
