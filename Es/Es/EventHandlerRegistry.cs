using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Exception;

namespace Es
{
    public class EventHandlerRegistry : IHandlerRegistry<IEventHandler>
    {
        private Dictionary<string, List<IEventHandler>> _handlers;
        
        public EventHandlerRegistry(IEnumerable<IEventHandler> handlers)
        {
            _handlers = new Dictionary<string, List<IEventHandler>>();
            foreach (var handler in handlers)
            {
                AddHandler(handler.Supports(), handler);
            }
        }
        
        public void AddHandler(string key, IEventHandler handler)
        {
            if (HasHandlers(key))
            {
                GetHandlers(key).Add(handler);
                return;
            }
            
            _handlers.Add(key, new List<IEventHandler>(){handler});
        }

        public bool HasHandlers(string key)
        {
            return _handlers.ContainsKey(key);
        }

        public List<IEventHandler> GetHandlers(string key)
        {
            return _handlers[key];
        }

        public int Size()
        {
            return _handlers.Count;
        }
    }
}