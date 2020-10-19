using System.Collections.Generic;
using System.Security;

namespace Es
{
    public interface ICommandHandlerRegistry
    {
        public void AddHandler(string key, ICommandHandler handler);
        public bool HasHandlers(string key);
        public List<ICommandHandler> GetHandlers(string key);
        public int Size();
    }
}