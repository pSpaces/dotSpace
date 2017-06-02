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
    }
}
