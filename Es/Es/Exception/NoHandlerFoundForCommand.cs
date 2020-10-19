using System;

namespace Es.Exception
{
    public class NoHandlerFoundForCommand : System.Exception
    {
        private const string message = "No handlers found for object";
        public NoHandlerFoundForCommand(object o) : base(String.Format("{0} {1}", message, o))
        {
        }
    }
}