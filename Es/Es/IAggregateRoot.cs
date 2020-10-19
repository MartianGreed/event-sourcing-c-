namespace Es
{
    public interface IAggregateRoot
    {
        public string GetAggregateId();

        public void GetUnCommitedEvents();
    }
}