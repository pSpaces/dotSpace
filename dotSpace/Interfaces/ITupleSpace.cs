using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotSpace.Interfaces
{
    public interface ITupleSpace
    {
        ITuple Get(IPattern pattern);
        ITuple GetP(IPattern pattern);
        ITuple Query(IPattern pattern);
        ITuple QueryP(IPattern pattern);
        void Put(ITuple t);

        ITuple Get(params object[] values);
        ITuple GetP(params object[] values);
        ITuple Query(params object[] values);
        ITuple QueryP(params object[] values);
        void Put(params object[] values);
    }
}
