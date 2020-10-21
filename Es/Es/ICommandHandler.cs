namespace Es
{
    public interface ICommandHandler : IHandler
    {
        public void Handle(ICommand command);

        public string Supports();
    }
}