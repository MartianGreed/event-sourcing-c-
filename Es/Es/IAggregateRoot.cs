using System.Collections.Generic;
using Es.Domain;

namespace Es
{
    public interface IAggregateRoot
    {
        public string GetAggregateId();

        public DomainEventStream GetUnCommitedEvents();

        public void InitializeState(DomainEventStream stream);
        
        public void Apply(IEvent e);
    }
}