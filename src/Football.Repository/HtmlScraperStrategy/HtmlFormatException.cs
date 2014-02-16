using System;

namespace Football.Repository
{
    public class HtmlFormatException : Exception
    {
        public HtmlFormatException(string message)
            : base(message)
        { }

        public HtmlFormatException()
            : this("The html provided is not valid or is empty.")
        { }
    }
}