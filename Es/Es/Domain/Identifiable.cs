using System;

namespace Es.Domain
{
    public interface IIdentity<out T> where T : class
    {
        static T Generate() => throw new NotImplementedException();

        static T FromString(string uuid) => throw new NotImplementedException();

        public string ToString();
    }
}