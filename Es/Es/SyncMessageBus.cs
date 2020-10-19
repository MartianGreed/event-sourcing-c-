using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Es.Exception;

namespace Es
{
    public class SyncMessageBus : IMessageBus
    {
        private Dictionary<string, ICommandHandler> _handlers;

        public SyncMessageBus(Dictionary<string, ICommandHandler> handlers)
        {
            _handlers = handlers;
        }

        public void Handle<T>(T arg)
        {
            var handlerKey = this.getHandlerClass(arg);
            if (this._handlers.ContainsKey(handlerKey))
            {
                this._handlers[handlerKey].Handle((ICommand) arg);
                return;
            }

            throw new NoHandlerFoundForCommand(arg);
        }

        private string getHandlerClass<T>(T arg)
        {
            var commandClass = TypeDescriptor.GetClassName(arg).Split(".").Last();
            return $"{commandClass}Handler";
        }
    }
}