using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;

namespace dotSpace.Objects.Network
{
    public class ClientNode : NodeBase
    {
        public override ITuple Get(string identifier, IPattern pattern)
        {
            return null;
        }
        public override ITuple GetP(string identifier, IPattern pattern)
        {
            return null;
        }
        public override ITuple Query(string identifier, IPattern pattern)
        {
            return null;
        }
        public override ITuple QueryP(string identifier, IPattern pattern)
        {
            return null;
        }
        public override void Put(string identifier, ITuple t)
        {

        }

        protected override T Decode<T>(string msg)
        {
            BasicResponse breq = msg.Deserialize<BasicResponse>();
            switch (breq.Action)
            {
                case ActionType.PUT_RESPONSE: return msg.Deserialize<PutResponse>() as T;
                case ActionType.GET_RESPONSE: return msg.Deserialize<GetResponse>() as T;
                case ActionType.QUERY_RESPONSE: return msg.Deserialize<GetResponse>() as T;
            }

            return default(T);
        }



    }
}
