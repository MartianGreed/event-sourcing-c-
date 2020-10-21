using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Es.Domain;

namespace Es
{
    public abstract class EventSourcedAggregateRoot : IAggregateRoot
    {
        private int _playhead = -1;
        private IEnumerable<KeyValuePair<int, DomainMessage>> _uncomittedEvents = new List<KeyValuePair<int, DomainMessage>>();
        
        public abstract string GetAggregateId();

        public DomainEventStream GetUnCommitedEvents()
        {
            DomainEventStream stream = DomainEventStream.FromKeyValuePair(_uncomittedEvents);
            _uncomittedEvents = new List<KeyValuePair<int, DomainMessage>>();
            return stream;
        }

        public void InitializeState(DomainEventStream stream)
        {
            foreach (var eventItem in stream)
            {
                ++_playhead;
                HandleRecursively(eventItem.Value.Payload);
            }
        }

        public void Apply(IEvent e)
        {
            HandleRecursively(e);
            
            ++_playhead;
            _uncomittedEvents = _uncomittedEvents.Append(
                new KeyValuePair<int, DomainMessage>(
                    _playhead,
                    DomainMessage.RecordNow(GetAggregateId(), _playhead, e, DateTimeImmutable.Now())
                )
            );
        }

        protected void Handle(IEvent e)
        {
            string eventName = TypeDescriptor.GetClassName(e).Split(".").Last();
            MethodInfo applyMethod = this.GetType().GetMethod($"Apply{eventName}");
            if (null == applyMethod)
            {
                return;
            }
            
            applyMethod.Invoke(this, new[] {e});
        }

        protected void HandleRecursively(IEvent e)
        {
            Handle(e);

            foreach (var childEntity in GetChildEntities())
            {
                childEntity.RegisterAggregateRoot(this);
                childEntity.HandleRecursively(e);
            }
        }

        protected IEnumerable<IEntity> GetChildEntities()
        {
            return new List<IEntity>();
        }
    }
}