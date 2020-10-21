using System.Collections.Generic;
using System.Security;

namespace Es
{
    public interface IQueryHandlerRegistry
    {
        public IQueryHandler GetHandler(string key);
    }
}