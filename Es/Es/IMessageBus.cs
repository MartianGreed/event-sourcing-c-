namespace Es
{
    public interface IMessageBus
    {
        void Handle<T>(T arg);
    }
}