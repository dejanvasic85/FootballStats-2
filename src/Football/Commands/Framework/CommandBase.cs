﻿using System;
using Football.Services;

namespace Football.Commands
{
    /// <summary>
    /// Serves as a template for all commands
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        protected CommandArguments Arguments;
        protected ILogService LogService;

        protected CommandBase(ILogService logService)
        {
            LogService = logService;
        }

        public bool HandleArguments(CommandArguments args)
        {
            // Store the arguments for future use
            Arguments = args;

            try
            {
                // Bubble the call through for any specific handling by the job
                ProcessArguments(args);
            }
            catch (ArgumentException ex)
            {
                LogService.Warning(ex.Message);
                return false;
            }
            return true;
        }

        public void Run()
        {
            LogService.Info(string.Format("\nStarting Job '{0}' ...\n", Arguments.CommandName));

            // Bubble the call through
            try
            {
                Go();

                LogService.Info("\nJob Complete\n");
            }
            catch (Exception e)
            {
                LogService.Error(e, verbose: true);
            }
        }

        protected abstract void ProcessArguments(CommandArguments args);
        protected abstract void Go();
    }
}