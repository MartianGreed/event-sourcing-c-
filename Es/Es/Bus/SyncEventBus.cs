using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Domain;
using Es.Exception;

namespace Es.Bus
{
    public class SyncEventBus : IEventBus
    {
        private bool _hasHandlers;
        private IHandlerRegistry<IEventHandler> _registry;

        public SyncEventBus()
        {
            _hasHandlers = false;
        }
        public void Publish(DomainEventStream stream)
        {
            if (!_hasHandlers)
            {
                throw new HandlersNotRegisteredException();
            }

            foreach (var domainEvent in stream)
            {
                IEvent payloadEvent = domainEvent.Value.Payload; 
                List<IEventHandler> handlers = _registry.GetHandlers(TypeDescriptor.GetClassName(payloadEvent).Split(".").Last().Replace("Handler", ""));
                foreach (IEventHandler handler in handlers)
                {
                    handler.Handle(payloadEvent);
                }
            }
        }
        
        public void SetHandlerRegistry(IHandlerRegistry<IEventHandler> registry)
        {
            _hasHandlers = true;
            _registry = registry;
        }
    }
}