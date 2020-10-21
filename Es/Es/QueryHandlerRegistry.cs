using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Exception;

namespace Es
{
    public class QueryHandlerRegistry: IHandlerRegistry<IQueryHandler>
    {
        private Dictionary<string, List<IQueryHandler>> _handlers;

        public QueryHandlerRegistry(IEnumerable<IQueryHandler> handlers)
        {
            _handlers = new Dictionary<string, List<IQueryHandler>>();
            foreach (var handler in handlers)
            {
                string key = TypeDescriptor.GetClassName(handler).Split(".").Last().Replace("Handler", "");
                AddHandler(key, handler);
            }
        }

        public void AddHandler(string key, IQueryHandler handler)
        {    
            if (HasHandlers(key))
            {
                GetHandlers(key).Add(handler);
                return;
            }
            
            _handlers.Add(key, new List<IQueryHandler>(){handler});
        }

        public List<IQueryHandler> GetHandlers(string key)
        {
            if (!HasHandlers(key))
            {
                throw new NoQueryHandlerRegisterForQuery(key);
            }

            return _handlers[key];
        }

        public bool HasHandlers(string key)
        {
            return _handlers.ContainsKey(key);
        }

        public int Size()
        {
            return _handlers.Count;
        }
    }
}