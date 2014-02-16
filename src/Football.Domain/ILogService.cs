using System;

namespace Football.Services
{
    public interface ILogService
    {
        void Log(string message);
        void Error(string message);
        void Error(Exception exception, bool verbose = false);
        void Warning(string message);
        void Info(string message);
    }
}