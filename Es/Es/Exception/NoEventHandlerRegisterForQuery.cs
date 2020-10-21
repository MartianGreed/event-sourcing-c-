using System;

namespace Es.Exception
{
    public class NoEventHandlerRegisterForQuery : System.Exception
    {
        private const string message = "No event handler found for object";
        public NoEventHandlerRegisterForQuery(object o) : base(String.Format("{0} {1}", message, o))
        {
        }
    }
}