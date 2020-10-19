using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Exception;

namespace Es
{
    public class CommandBus : ICommandBus
    {
        private bool _hasHandlers;
        private ICommandHandlerRegistry _registry;

        public CommandBus()
        {
            _hasHandlers = false;
        }
        
        public void Dispatch(ICommand command)
        {
            if (!_hasHandlers)
            {
                throw new HandlersNotRegisteredException();
            }

            IEnumerable<ICommandHandler> handlers = _registry.GetHandlers(this.getHandlerClass(command));

            foreach (var handler in handlers)
            {
                handler.Handle(command);
            }
        }

        public void SetHandlerRegistry(ICommandHandlerRegistry registry)
        {
            _hasHandlers = true;
            _registry = registry;
        }
        
        private string getHandlerClass(ICommand arg)
        {
            return TypeDescriptor.GetClassName(arg).Split(".").Last();
        }
    }
}