using System;
using System.Linq;
using Football.Commands;
using Football.Repository;
using Football.Services;
using Microsoft.Practices.Unity;

namespace Football
{
    class Program
    {
        private static IUnityContainer _unityContainer;
        private static readonly string[] ExitKeywords = { "exit", "quit", "q" };
        private static readonly string[] HelpKeywords = { "help", "h", "?" };

        static void Main(string[] args)
        {
            // Show welcome 
            Console.WindowWidth = 120;
            Console.WriteLine("Welcome to the command line tool.");
            Console.WriteLine("Start executing e.g. -exe ShowChampion");
            Console.Title = "Football Stats";

            // Start :)
            try
            {
                // Let's get our DI container prepared
                RegisterDependencies();

                // Start the program
                Program program = new Program();
                program.Start(args);
            }
            catch (Exception ex)
            {
                // If it gets to here then something really bad has happened
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
        }

        void Start(string[] args)
        {
            // Show the help menu - just to be nice and make use of the Help Attribute
            ShowHelp();

            // This flag allows us to run the program from command line
            bool firstTime = true;

            // Keep executing commands until the user opts out
            while (firstTime || (args.Length > 0 && args[0].NotEqualToAny(ExitKeywords)))
            {
                if (!firstTime && HelpKeywords.Any(help => help.Equals(args[0], StringComparison.OrdinalIgnoreCase)))
                {
                    ShowHelp();
                }
                else
                {
                    Execute(args);
                }

                Console.Write("GO> ");
                args = Console.ReadLine().Split(' ');

                firstTime = false;
            }
        }

        void Execute(string[] args)
        {
            // Sequence: 1. Convert to command args, 2. Resolve the command, 3. Execute 
            // This should be moved in a separate class ( and potentially create a third party library and host on nuget!)

            if (args.Length == 0)
                return;

            CommandArguments parsedArgs;
            var logService = _unityContainer.Resolve<ILogService>();

            try
            {
                parsedArgs = CommandArguments.FromArray(args);
            }
            catch (Exception e)
            {
                // Simply print out the error to the user
                logService.Error(e);
                return;
            }

            // Attempt to resolve the command
            var command = _unityContainer
                .ResolveAll<ICommand>()
                .SingleOrDefault(c => c.GetType().Name.Equals(parsedArgs.CommandName, StringComparison.OrdinalIgnoreCase));

            if (command == null)
            {
                logService.Warning(string.Format("The command name '{0}' does not exist. Please try again", parsedArgs.CommandName));
                return;
            }

            // Finally - Run the command right after handling args!
            if (command.HandleArguments(parsedArgs))
                command.Run();
        }

        static void RegisterDependencies()
        {
            // Register all services and repositories ( ready for commands )
            _unityContainer = new UnityContainer()
                .RegisterType<ILogService, ConsoleLogService>()
                .RegisterType<ITeamService, TeamService>()
                .RegisterType<ITeamRepository, TeamCsvRepository>(new InjectionConstructor("Artefacts/football.csv"));

            // Register all types that implement ICommand
            // This makes it nice that developers can create new commands and they are auto registered
            CommandRegistrations.ActionEach(command => _unityContainer.RegisterType(typeof(ICommand), command, command.Name));
        }

        static void ShowHelp()
        {
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("-------------------");
            
            CommandRegistrations.ActionEach(type => Console.WriteLine(type.Name.PadRight(35) + type.GetCustomAttribute<HelpAttribute>().Description));
            Console.WriteLine("\n");
        }
    }
}
