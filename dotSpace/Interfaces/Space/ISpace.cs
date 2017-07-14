using System.Collections.Generic;

namespace dotSpace.Interfaces.Space
{
    /// <summary>
    /// Defines the minimal required operations that a tuple space datastructure should support. 
    /// </summary>
    public interface ISpace
    {
        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Get(IPattern pattern);

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Get(params object[] pattern);

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        ITuple GetP(IPattern pattern);

        /// <summary>
        /// Retrieves and removes the first tuple from the space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        ITuple GetP(params object[] pattern);

        /// <summary>
        /// Retrieves and removes all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> GetAll(IPattern pattern);

        /// <summary>
        /// Retrieves and removes all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> GetAll(params object[] pattern);

        /// <summary>
        /// Retrieves a clone of the first tuple from the space matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Query(IPattern pattern);

        /// <summary>
        /// Retrieves a clone of the first tuple from the space matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Query(params object[] pattern);

        /// <summary>
        /// Retrieves a clone of the first tuple from the space matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        ITuple QueryP(IPattern pattern);

        /// <summary>
        /// Retrieves a clone of the first tuple from the space matching the specified pattern.The operation is non-blocking.The operation will return null if no elements match.
        /// </summary>
        ITuple QueryP(params object[] pattern);

        /// <summary>
        /// Retrieves clones of all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> QueryAll(IPattern pattern);

        /// <summary>
        /// Retrieves clones of all tuples from the space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> QueryAll(params object[] pattern);

        /// <summary>
        /// Inserts the tuple passed as argument into the space.
        /// </summary>
        void Put(ITuple tuple);

        /// <summary>
        /// Inserts the tuple passed as argument into the space.
        /// </summary>
        void Put(params object[] tuple);
    }
}
