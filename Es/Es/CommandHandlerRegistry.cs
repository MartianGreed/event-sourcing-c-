using System.Collections.Generic;
using Es.Exception;

namespace Es
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private Dictionary<string, List<ICommandHandler>> _handlers;

        public CommandHandlerRegistry(IEnumerable<ICommandHandler> handlers)
        {
            _handlers = new Dictionary<string, List<ICommandHandler>>();
            foreach (var handler in handlers)
            {
                AddHandler(handler.Supports(), handler);
            }
        }
        
        public void AddHandler(string key, ICommandHandler handler)
        {
            if (this.HasHandlers(key))
            {
                this.GetHandlers(key).Add(handler);
                return;
            }
            
            _handlers.Add(key, new List<ICommandHandler>(){handler});
        }

        public bool HasHandlers(string key)
        {
            return _handlers.ContainsKey(key);
        }

        public List<ICommandHandler> GetHandlers(string key)
        {
            if (!this.HasHandlers(key))
            {
                throw new NoCommandyHandlerRegisterForCommand(key);
            }
            return _handlers[key];
        }

        public int Size()
        {
            return _handlers.Count;
        }
    }
}