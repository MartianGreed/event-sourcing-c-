using System.Collections.Generic;

namespace Es
{
    public interface IHandlerRegistry<T> where T : IHandler
    {
        public void AddHandler(string key, T handler);
        public bool HasHandlers(string key);
        public List<T> GetHandlers(string key);
        public int Size();
    }
}