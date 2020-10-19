using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Es.Exception;

namespace Es.Domain
{
    // Represent a stream of domain events that are retrieved from the event store.
    public class DomainEventStream : IEnumerable<KeyValuePair<int, DomainMessage>>
    {
        private readonly Dictionary<int, DomainMessage> _domainMessages;

        private DomainEventStream(Dictionary<int, DomainMessage> stream)
        {
            _domainMessages = stream;
        }
        
        public DomainEventStream AppendStream(DomainEventStream stream)
        {
            foreach (var message in stream)
            {
                if (_domainMessages.ContainsKey(message.Value.Playhead))
                {
                    throw new DuplicatedPlayheadException();
                }
                _domainMessages.Add(message.Value.Playhead, message.Value);
            }

            return this;
        }

        public static DomainEventStream FromKeyValuePair(IEnumerable<KeyValuePair<int, DomainMessage>> stream)
        {
            var domainEventStream = new Dictionary<int, DomainMessage>();
            foreach (var message in stream)
            {
                domainEventStream.Add(message.Value.Playhead, message.Value);
            }
            
            return new DomainEventStream(domainEventStream);
        }

        public IEnumerator<KeyValuePair<int, DomainMessage>> GetEnumerator()
        {
            return _domainMessages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Size()
        {
            return _domainMessages.Count;
        }
    }
}