using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Exception;

namespace Es
{
    public class QueryHandlerRegistry: IQueryHandlerRegistry
    {
        private Dictionary<string, IQueryHandler> _handlers;

        public QueryHandlerRegistry(IEnumerable<IQueryHandler> handlers)
        {
            _handlers = new Dictionary<string, IQueryHandler>();
            foreach (var handler in handlers)
            {
                AddHandler(handler);
            }
        }

        public void AddHandler(IQueryHandler handler)
        {    
            _handlers.Add(TypeDescriptor.GetClassName(handler).Split(".").Last().Replace("Handler", ""), handler);
        }

        public bool HasHandler(string key)
        {
            return _handlers.ContainsKey(key);
        }

        public IQueryHandler GetHandler(string key)
        {
            if (!this.HasHandler(key))
            {
                throw new NoQueryHandlerRegisterForQuery(key);
            }

            return _handlers[key];
        }

        public int Size()
        {
            return _handlers.Count;
        }
    }
}