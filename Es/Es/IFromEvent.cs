namespace Es
{
    public interface IFromEvent<T, U> where T : IEvent where U : IReadModel
    {
        public static U FromEvent(T ev) => throw new System.NotImplementedException();
    }
}