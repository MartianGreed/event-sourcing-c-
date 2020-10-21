using System;
using Microsoft.Extensions.DependencyInjection;

namespace Es.Domain
{
    public class Identity<T> : IIdentity<T> where T : class
    {
        private readonly Guid _uuid;

        public Identity(Guid uuid)
        {
            _uuid = uuid;
        }
        
        public static T Generate()
        {
            T t = (T) Activator.CreateInstance(typeof(T), Guid.NewGuid());
            return t;
        }

        public static T FromString(string uuid)
        {
            T t = (T) Activator.CreateInstance(typeof(T), Guid.Parse(uuid));
            return t;
        }

        public override string ToString()
        {
            return _uuid.ToString();
        }
    }
}