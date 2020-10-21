namespace Es
{
    public interface ICommandBus
    {
        void Dispatch(ICommand command);
        void SetHandlerRegistry(IHandlerRegistry<ICommandHandler> registry);
    }
}