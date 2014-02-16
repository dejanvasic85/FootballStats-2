using System;

namespace Football.Services
{
    public class ConsoleLogService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message)
        {
            PrintWithColour(message, ConsoleColor.Red);
        }
        
        public void Error(Exception exception, bool verbose = false)
        {
            Error(exception.Message);

            if (verbose)
                Error(exception.StackTrace);
        }

        public void Warning(string message)
        {
            PrintWithColour(message, ConsoleColor.Yellow);
        }

        public void Info(string message)
        {
            PrintWithColour(message, ConsoleColor.Cyan);
        }

        private void PrintWithColour(string message, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Log(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}