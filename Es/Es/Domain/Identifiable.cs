using System;

namespace Es.Domain
{
    public interface IIdentity
    {
        static IIdentity Generate() => throw new NotImplementedException();

        static IIdentity FromString(string uuid) => throw new NotImplementedException();

        public string ToString();
    }
}