using System;

namespace Es.Exception
{
    public class NoQueryHandlerRegisterForQuery : System.Exception
    {
        private const string message = "No query handler found for object";
        public NoQueryHandlerRegisterForQuery(object o) : base(String.Format("{0} {1}", message, o))
        {
        }
    }
}