using System.Collections.Generic;

namespace dotSpace.Interfaces
{
    public interface IEnumerableResult
    {
        IEnumerable<object[]> Result { get; set; }
    }
}
