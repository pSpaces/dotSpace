using System.Collections.Generic;

namespace dotSpace.Interfaces
{
    public interface ITupleSpace
    {
        ITuple Get(IPattern pattern);
        ITuple Get(params object[] values);
        ITuple GetP(IPattern pattern);
        ITuple GetP(params object[] values);
        IEnumerable<ITuple> GetAll(IPattern pattern);
        IEnumerable<ITuple> GetAll(params object[] values);
        ITuple Query(IPattern pattern);
        ITuple Query(params object[] values);
        ITuple QueryP(IPattern pattern);
        ITuple QueryP(params object[] values);
        IEnumerable<ITuple> QueryAll(IPattern pattern);
        IEnumerable<ITuple> QueryAll(params object[] values);
        void Put(ITuple t);
        void Put(params object[] values);
    }
}
