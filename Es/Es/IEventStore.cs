using Es.Domain;

namespace Es
{
    public interface IEventStore
    {
        public DomainEventStream Load(IIdentity id);

        public void Append(IIdentity id, DomainEventStream eventStream);

        public DomainEventStream LoadFromPlayhead(IIdentity id, int playhead);
    }
}