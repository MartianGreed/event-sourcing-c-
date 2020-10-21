namespace Es
{
    public interface IEntity
    {
        public void RegisterAggregateRoot(IAggregateRoot ar);
        public void HandleRecursively(IEvent e);
    }
}