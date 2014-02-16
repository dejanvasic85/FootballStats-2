using System;
using System.Linq;
using System.Reflection;

namespace Football.Commands
{
    public static class CommandRegistrations
    {
        /// <summary>
        /// Contains the registrations for all commands which are simply classes that implement ICommand
        /// </summary>
        private static readonly Lazy<Type[]> _commandTypes = new Lazy<Type[]>(() =>
        {
            var interfaceType = typeof(ICommand);
            var commandTypeList = Assembly.GetExecutingAssembly()
                            .GetTypes()
                            .Where(type => type.GetInterface(interfaceType.Name, false) == interfaceType && !type.IsAbstract)
                            .ToArray();

            return commandTypeList;
        });

        public static Type[] AvailableTasks()
        {
            return _commandTypes.Value;
        }

        /// <summary>
        /// Performs the required invocation of each action for all available commands
        /// </summary>
        public static void ActionEach(Action<Type> action)
        {
            AvailableTasks().ForEach(action);
        }
    }
}