using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace Pong
{
    /// <summary>
    /// Creates custom tuples based on the entity type.
    /// </summary>
    public class EntityFactory : ITupleFactory
    {
        public ITuple Create(params object[] fields)
        {
            switch ((int)fields[0])
            {
                case EntityType.PONG: return new Pong(fields);
                case EntityType.POSITION: return new Position(fields);
                case EntityType.PLAYERINFO: return new PlayerInfo(fields);
                case EntityType.SIGNAL: return new dotSpace.Objects.Space.Tuple(fields);
                default: return new dotSpace.Objects.Space.Tuple(fields);
            }
        }
    }
}
