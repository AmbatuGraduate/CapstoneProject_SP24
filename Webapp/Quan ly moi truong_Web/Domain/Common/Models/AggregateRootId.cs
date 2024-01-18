namespace Domain.Common.Models
{
    /// <summary>
    ///  For resolve the problem Identity Paradox
    /// </summary>
    /// <typeparam name="TId">Type of the Id</typeparam>

    public abstract class AggregateRootId<TId> : ValueObject
    {
        public abstract TId Value { get; protected set; }
    }
}