namespace Domain.Common.Models
{
    public class AggregateRoot<TId, TIdType> : Entity<TId>
        where TId : AggregateRootId<TIdType>
    {
        public new AggregateRootId<TIdType> Id { get; protected set; }

        public AggregateRoot(TId Id) : base(Id)
        {
            this.Id = Id;
        }
    }
}