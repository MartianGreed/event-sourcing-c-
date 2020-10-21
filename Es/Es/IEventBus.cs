using Es.Domain;

namespace Es
{
    public interface IEventBus
    {
        void Publish(DomainEventStream stream);
    }
}