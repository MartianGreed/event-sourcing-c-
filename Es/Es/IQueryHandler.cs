namespace Es
{
    public interface IQueryHandler : IHandler
    {
        QueryResult Handle(IQuery query);
    }
}