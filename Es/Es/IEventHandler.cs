namespace Es
{
    public interface IEventHandler : IHandler
    {
        void Handle(IEvent e);

        string Supports();
    }
}