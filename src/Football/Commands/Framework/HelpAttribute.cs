using System;

namespace Football.Commands
{
    /// <summary>
    /// Used for displaying the help menu so apply this attribute on any class implementing the ICommand
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class HelpAttribute : Attribute
    {
        /// <summary>
        /// This is the detailed information for the user
        /// </summary>
        public string Description { get; set; }
    }
}