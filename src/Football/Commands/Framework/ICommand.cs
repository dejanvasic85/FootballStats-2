namespace Football.Commands
{
    public interface ICommand
    {
        bool HandleArguments(CommandArguments args);
        void Run();
    }
}
