using System.Collections.Generic;

namespace dotSpace.Interfaces
{
    public interface ISpace
    {
        ITuple Get(IPattern pattern);
        ITuple Get(params object[] pattern);
        ITuple GetP(IPattern pattern);
        ITuple GetP(params object[] pattern);
        IEnumerable<ITuple> GetAll(IPattern pattern);
        IEnumerable<ITuple> GetAll(params object[] pattern);
        ITuple Query(IPattern pattern);
        ITuple Query(params object[] pattern);
        ITuple QueryP(IPattern pattern);
        ITuple QueryP(params object[] pattern);
        IEnumerable<ITuple> QueryAll(IPattern pattern);
        IEnumerable<ITuple> QueryAll(params object[] pattern);
        void Put(ITuple tuple);
        void Put(params object[] tuple);
    }
}
