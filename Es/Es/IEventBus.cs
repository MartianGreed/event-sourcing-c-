namespace Es
{
    public interface IEventBus
    {
        void Dispatch<T>(IEvent e);
    }
}