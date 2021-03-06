namespace Es
{
    public interface IQueryBus
    {
        public IQueryResult Ask(IQuery query);
        void SetHandlerRegistry(IHandlerRegistry<IQueryHandler> registry);
    }
}