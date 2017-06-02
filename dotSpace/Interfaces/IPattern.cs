namespace dotSpace.Interfaces
{
    public interface IPattern : IFields
    {
        bool Match(IFields entity);
    }
}
