using System.Collections.Generic;
using System.Security;

namespace Es
{
    public interface ICommandHandlerRegistry<T> : IHandlerRegistry<T> where T : IHandler
    {
        
    }
}