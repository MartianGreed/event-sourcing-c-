using Es.Domain;

namespace Es
{
    public class EventSourcedRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        private readonly IEventStore _eventStore;
        private readonly IEventBus _eventBus;
        private readonly IAggregateFactory<T> _aggregateFactory;

        public EventSourcedRepository(IEventStore eventStore, IEventBus eventBus, IAggregateFactory<T> aggregateFactory)
        {
            _eventStore = eventStore;
            _eventBus = eventBus;
            _aggregateFactory = aggregateFactory;
        }
        
        public T Load(string id)
        {
            DomainEventStream stream = _eventStore.Load(id);

            return _aggregateFactory.Create(stream);
        }

        public void Save(T aggregateRoot)
        {
            DomainEventStream stream = aggregateRoot.GetUnCommitedEvents();
            _eventStore.Append(aggregateRoot.GetAggregateId(), stream);
            _eventBus.Publish(stream);
        }
    }
}