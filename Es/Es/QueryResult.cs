namespace Es
{
    public class QueryResult : IQueryResult
    {
        public object Data { get; }

        public QueryResult(object data)
        {
            Data = data;
        }
    }
}