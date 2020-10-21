namespace Es
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        public T Load(string id);

        public void Save(T aggregateRoot);
    }
}