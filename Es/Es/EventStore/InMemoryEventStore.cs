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

        public DomainEventStream Load(IIdentity id)
        {
            CheckEventStoreHasAggregate(id.ToString());

            return _events[id.ToString()];
        }

        public void Append(IIdentity id, DomainEventStream eventStream)
        {
            try
            {
                CheckEventStoreHasAggregate(id.ToString());
            }
            catch (AggregateNotFoundException)
            {
                _events[id.ToString()] = eventStream;
                return;
            }

            _events[id.ToString()] = _events[id.ToString()].AppendStream(eventStream);
        }

        public DomainEventStream LoadFromPlayhead(IIdentity id, int playhead)
        {
            CheckEventStoreHasAggregate(id.ToString());
            
            return DomainEventStream.FromKeyValuePair(_events[id.ToString()].Where(ds => playhead <= ds.Value.Playhead));
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