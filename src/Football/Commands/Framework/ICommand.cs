namespace Football.Commands
{
    public interface ICommand
    {
        void HandleArguments(CommandArguments args);
        void Run();
    }
}
