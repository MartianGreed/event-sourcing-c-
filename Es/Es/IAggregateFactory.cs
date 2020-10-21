using Es.Domain;

namespace Es
{
    public interface IAggregateFactory<T> where T : IAggregateRoot
    {
        public T Create(DomainEventStream stream);
    }
}