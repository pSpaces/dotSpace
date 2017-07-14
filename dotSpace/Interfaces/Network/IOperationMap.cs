namespace dotSpace.Interfaces.Network
{
    /// <summary>
    /// This interface specifies the operations required to map requests with operations on a space repository.
    /// </summary>
    public interface IOperationMap
    {
        /// <summary>
        /// Method executing an operation based on the passed request.
        /// </summary>
        IMessage Execute(IMessage request);
    }
}
