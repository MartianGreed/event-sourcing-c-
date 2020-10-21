namespace Es.Exception
{
    public class FailedToInstanciateAggregateRootException : System.Exception
    {
        public FailedToInstanciateAggregateRootException() : base(
            "Failed to instanciate aggregate root from reflection")
        {
            
        }
    }
}