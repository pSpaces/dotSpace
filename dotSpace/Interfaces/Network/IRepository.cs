using dotSpace.Interfaces.Space;
using System.Collections.Generic;

namespace dotSpace.Interfaces.Network
{
    /// <summary>
    /// Defines the methods that allow operations on multiple tuple spaces.
    /// These methods are used for supporting networked tuple spaces.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Adds a new Gate to the repository based on the provided uri.
        /// </summary>
        void AddGate(string uri);
        /// <summary>
        /// Closes the gate represented by the specific connectionstring, and terminates the underlying thread.
        /// </summary>
        void CloseGate(string connectionString);
        /// <summary>
        /// Adds a new Space to the repository, identified by the specified parameter.
        /// </summary>
        void AddSpace(string identifier, ISpace tuplespace);
        /// <summary>
        /// Returns the local instance of the space identified by the parameter.
        /// </summary>
        ISpace GetSpace(string target);
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Get(string target, IPattern pattern);
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Get(string target, params object[] pattern);
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        ITuple GetP(string target, IPattern pattern);
        /// <summary>
        /// Retrieves and removes the first tuple from the target Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        ITuple GetP(string target, params object[] pattern);
        /// <summary>
        /// Retrieves and removes all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> GetAll(string target, IPattern pattern);
        /// <summary>
        /// Retrieves and removes all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> GetAll(string target, params object[] pattern);
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Query(string target, IPattern pattern);
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern. The operation will block if no elements match.
        /// </summary>
        ITuple Query(string target, params object[] pattern);
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern. The operation is non-blocking. The operation will return null if no elements match.
        /// </summary>
        ITuple QueryP(string target, IPattern pattern);
        /// <summary>
        /// Retrieves the first tuple from the target Space, matching the specified pattern.The operation is non-blocking.The operation will return null if no elements match.
        /// </summary>
        ITuple QueryP(string target, params object[] pattern);
        /// <summary>
        /// Retrieves all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> QueryAll(string target, IPattern pattern);
        /// <summary>
        /// Retrieves all tuples from the target Space matching the specified pattern. The operation is non-blocking. The operation will return an empty set if no elements match.
        /// </summary>
        IEnumerable<ITuple> QueryAll(string target, params object[] pattern);
        /// <summary>
        /// Inserts the tuple passed as argument into the target Space.
        /// </summary>
        void Put(string target, ITuple tuple);
        /// <summary>
        /// Inserts the tuple passed as argument into the target Space.
        /// </summary>
        void Put(string target, params object[] tuple);
    }
}
