namespace dotSpace.Interfaces
{
    public interface IPattern : IFields
    {
        int Size { get; }
        object this[int idx] { get; }
    }
}
