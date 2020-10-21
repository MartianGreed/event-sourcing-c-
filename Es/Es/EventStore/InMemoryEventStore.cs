using System.Collections.Generic;
using System.Linq;
using Es.Domain;
using Es.Exception;

namespace Es.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private Dictionary<string, DomainEventStream> _events;

        public InMemoryEventStore()
        {
            _events = new Dictionary<string, DomainEventStream>();
        }

        public DomainEventStream Load(string id)
        {
            CheckEventStoreHasAggregate(id);

            return _events[id];
        }

        public void Append(string id, DomainEventStream eventStream)
        {
            try
            {
                CheckEventStoreHasAggregate(id);
            }
            catch (AggregateNotFoundException)
            {
                _events[id] = eventStream;
                return;
            }

            _events[id] = _events[id].AppendStream(eventStream);
        }

        public DomainEventStream LoadFromPlayhead(string id, int playhead)
        {
            CheckEventStoreHasAggregate(id);
            
            return DomainEventStream.FromKeyValuePair(_events[id].Where(ds => playhead <= ds.Value.Playhead));
        }

        private void CheckEventStoreHasAggregate(string id)
        {
            if (!_events.ContainsKey(id))
            {
                throw new AggregateNotFoundException();
            }
        }
    }
}