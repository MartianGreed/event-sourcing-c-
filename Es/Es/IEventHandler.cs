namespace Es
{
    public interface IEventHandler
    {
        void Handle(IEvent e);
    }
}