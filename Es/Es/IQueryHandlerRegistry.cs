using System.Collections.Generic;
using System.Security;

namespace Es
{
    public interface IQueryHandlerRegistry
    {
        public void AddHandler(IQueryHandler handler);
        public bool HasHandler(string key);
        public IQueryHandler GetHandler(string key);
        public int Size();
    }
}