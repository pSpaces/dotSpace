using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;

namespace Lifeforms
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
                case EntityType.POSITION: return new Position(fields);
                case EntityType.SPAWN: return new SpawnLifeform(fields);
                case EntityType.FOOD: return new Food(fields);
                case EntityType.LIFEFORM_STATS: return new LifeformStats(fields);
                case EntityType.SIGNAL: return new dotSpace.Objects.Space.Tuple(fields);
                default: return new dotSpace.Objects.Space.Tuple(fields);
            }
        }
    }
}
