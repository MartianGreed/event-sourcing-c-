using System;

namespace Es.Exception
{
    public class NoCommandyHandlerRegisterForCommand : System.Exception
    {
        private const string message = "No query handler found for object";
        public NoCommandyHandlerRegisterForCommand(object o) : base(String.Format("{0} {1}", message, o))
        {
        }
    }
}