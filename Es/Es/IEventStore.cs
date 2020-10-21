using Es.Domain;

namespace Es
{
    public interface IEventStore
    {
        public DomainEventStream Load(string id);

        public void Append(string id, DomainEventStream eventStream);

        public DomainEventStream LoadFromPlayhead(string id, int playhead);
    }
}