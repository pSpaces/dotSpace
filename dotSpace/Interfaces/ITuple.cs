namespace dotSpace.Interfaces
{
    public interface ITuple : IFields
    {
        int Size { get; }

        object this[int idx] { get; set; }
    }
}
