using System.Collections.Generic;

namespace dotSpace.Interfaces
{
    public interface IRepository
    {
        void AddGate(string uri);
        void AddSpace(string identifier, ISpace tuplespace);
        ISpace GetSpace(string target);
        ITuple Get(string target, IPattern pattern);
        ITuple Get(string target, params object[] pattern);
        ITuple GetP(string target, IPattern pattern);
        ITuple GetP(string target, params object[] pattern);
        IEnumerable<ITuple> GetAll(string target, IPattern pattern);
        IEnumerable<ITuple> GetAll(string target, params object[] pattern);
        ITuple Query(string target, IPattern pattern);
        ITuple Query(string target, params object[] pattern);
        ITuple QueryP(string target, IPattern pattern);
        ITuple QueryP(string target, params object[] pattern);
        IEnumerable<ITuple> QueryAll(string target, IPattern pattern);
        IEnumerable<ITuple> QueryAll(string target, params object[] pattern);
        void Put(string target, ITuple tuple);
        void Put(string target, params object[] tuple);
    }
}
