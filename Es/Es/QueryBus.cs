using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Exception;

namespace Es
{
    public class QueryBus : IQueryBus
    {
        private bool _hasHandlers;
        private IHandlerRegistry<IQueryHandler> _registry;

        public QueryBus()
        {
            _hasHandlers = false;
        }
        public IQueryResult Ask(IQuery query)
        {
            if (!_hasHandlers)
            {
                throw new HandlersNotRegisteredException();
            }
            
            string handlerClass = GetHandlerClass(query);

            IQueryHandler handler = _registry.GetHandlers(handlerClass).First();
            
            return handler.Handle(query);
        }

        public void SetHandlerRegistry(IHandlerRegistry<IQueryHandler> registry)
        {
            _hasHandlers = true;
            _registry = registry;
        }

        private string GetHandlerClass(IQuery arg)
        {
            return TypeDescriptor.GetClassName(arg).Split(".").Last();
        }
    }
}