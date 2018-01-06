using dotSpace.Interfaces.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace dotSpace.BaseClasses.Space
{
    /// <summary>
    /// Provides the basic functionality for a tuplespace datastructure. 
    /// Represents a strongly typed set of tuples that can be access through pattern matching. Provides methods to query and manipulate the set.
    /// This class does not impose ordering on the underlying tuples.
    /// This is an abstract class.
    /// </summary>
    public abstract class SpaceBase : ISpace
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private readonly Dictionary<ulong, List<ITuple>> buckets;
        private readonly Dictionary<ulong, ReaderWriterLockSlim> bucketLocks;
        private readonly object bucketAccess;
        private readonly ITupleFactory tupleFactory;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        /// <summary>
        /// Initializes a new instance of the SpaceBase class. All tuples will be created using the provided tuple factory.
        /// </summary>
        public SpaceBase(ITupleFactory tupleFactory)
        {
            this.buckets = new Dictionary<ulong, List<ITuple>>();
            this.bucketLocks = new Dictionary<ulong, ReaderWriterLockSlim>();
            this.bucketAccess = new object();
            this.tupleFactory = tupleFactory;
            if (this.tupleFactory == null)
            {
                throw new Exception("Must use a valid TupleFactory.");
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(IPattern pattern)
        {
            return this.Get(pattern.Fields);
        }

        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Get(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            ITuple t = this.WaitUntilMatch(bucket, bucketLock, pattern);
            // Guard against duplication from retrieval
            bool success = false;
            bucketLock.EnterWriteLock();
            success = bucket.Remove(t);
            bucketLock.ExitWriteLock();
            return success ? t : this.Get(pattern);
        }

        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(IPattern pattern)
        {
            return this.GetP(pattern.Fields);
        }

        /// <summary>
        /// Retrieves and removes the first tuple from the Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple GetP(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            ITuple t = this.Find(bucket, bucketLock, pattern);
            // Guard against duplication from retrieval
            bool success = false;
            if (t != null)
            {
                bucketLock.EnterWriteLock();
                success = bucket.Remove(t);
                bucketLock.ExitWriteLock();
            }
            return success ? t : null;
        }

        /// <summary>
        /// Retrieves and removes all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(IPattern pattern)
        {
            return this.GetAll(pattern.Fields);
        }

        /// <summary>
        /// Retrieves and removes all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> GetAll(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            IEnumerable<ITuple> results = this.FindAll(bucket, bucketLock, pattern);
            if (results != null)
            {
                bucketLock.EnterWriteLock();
                results = results.Where(x => bucket.Remove(x)).ToList();
                bucketLock.ExitWriteLock();
            }
            return results;
        }

        /// <summary>
        /// Retrieves a clone of the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(IPattern pattern)
        {
            return this.Query(pattern.Fields);
        }

        /// <summary>
        /// Retrieves a clone of the first tuple from the Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        public ITuple Query(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            return this.Copy(this.WaitUntilMatch(bucket, bucketLock, pattern));
        }

        /// <summary>
        /// Retrieves a clone of the first tuple from the Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(IPattern pattern)
        {
            return this.QueryP(pattern.Fields);
        }

        /// <summary>
        /// Retrieves a clone of the first tuple from the Space, matching the specified pattern.The operation is non-blocking.The operation will return null if no elements match.
        /// </summary>
        public ITuple QueryP(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);
            return this.Copy(this.Find(bucket, bucketLock, pattern));
        }

        /// <summary>
        /// Retrieves clones of all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(IPattern pattern)
        {
            return this.QueryAll(pattern.Fields);
        }

        /// <summary>
        /// Retrieves clones of all tuples from the Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        public IEnumerable<ITuple> QueryAll(params object[] pattern)
        {
            ulong hash = this.ComputeHash(pattern);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);
            return this.FindAll(bucket, bucketLock, pattern).Select(x => this.Copy(x));
        }

        /// <summary>
        /// Inserts the tuple passed as argument into the Space.
        /// </summary>
        public void Put(ITuple t)
        {
            this.Put(t.Fields);
        }

        /// <summary>
        /// Inserts the tuple passed as argument into the Space.
        /// </summary>
        public void Put(params object[] values)
        {
            ulong hash = this.ComputeHash(values);
            Monitor.Enter(this.bucketAccess);
            List<ITuple> bucket = this.GetBucket(hash);
            ReaderWriterLockSlim bucketLock = this.GetBucketLock(hash);
            Monitor.Exit(this.bucketAccess);

            bucketLock.EnterWriteLock();
            Monitor.Enter(bucket);
            bucket.Insert(this.GetIndex(bucket.Count), this.Copy(values));
            Monitor.PulseAll(bucket);
            Monitor.Exit(bucket);
            bucketLock.ExitWriteLock();
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Protected Methods

        /// <summary>
        /// Template method returning the index of where to insert a tuple.
        /// This method constitutes the ordering of the space.
        /// </summary>
        protected abstract int GetIndex(int size);

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private object[] CopyFields(object[] fields)
        {
            object[] newFields = new object[fields.Length];
            Array.Copy(fields, newFields, fields.Length);
            return newFields;
        }

        private ITuple Copy(object[] fields)
        {
            return this.tupleFactory.Create(this.CopyFields(fields));
        }

        private ITuple Copy(ITuple tuple)
        {
            return tuple != null ? this.tupleFactory.Create(this.CopyFields(tuple.Fields)) : null;
        }

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
            while ((t = this.Find(bucket, bucketLock, pattern, false)) == null)
            {
                Monitor.Enter(bucket);
                bucketLock.ExitReadLock();
                Monitor.Wait(bucket);
                Monitor.Exit(bucket);
            }
            bucketLock.ExitReadLock();
            return t;
        }
        private ITuple Find(List<ITuple> bucket, ReaderWriterLockSlim bucketLock, object[] pattern, bool exit = true)
        {
            bucketLock.EnterReadLock();
            ITuple t = bucket.Where(x => this.Match(pattern, x.Fields)).FirstOrDefault();
            if (exit) bucketLock.ExitReadLock();
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

        #endregion
    }
}
