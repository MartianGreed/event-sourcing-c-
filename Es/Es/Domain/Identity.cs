using System;

namespace Es.Domain
{
    public class Identity : IIdentity
    {
        private readonly Guid _uuid;

        private Identity(Guid uuid)
        {
            _uuid = uuid;
        }
        
        public static IIdentity Generate()
        {
            return new Identity(Guid.NewGuid());
        }

        public static IIdentity FromString(string uuid)
        {
            return new Identity(Guid.Parse(uuid));
        }

        public override string ToString()
        {
            return _uuid.ToString();
        }
    }
}