using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Football
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts line of text with a delimited to an array of strings
        /// </summary>
        public static string[] FromCsv(this string line, char delimiter = ',')
        {
            StringBuilder sb = new StringBuilder();
            bool isQuotes = false;
            bool isStartOfField = true;
            char escapeDelimiter = '\0';
            const char quote = '\"';

            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException();

            if (line.Contains(escapeDelimiter))
            {
                // Use other delimiter if this one is already found
                escapeDelimiter = '\b';
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == delimiter && !isQuotes)
                {
                    sb.Append(escapeDelimiter);
                    isStartOfField = true;
                    isQuotes = false;
                    continue;
                }

                if (line[i] == quote)
                {
                    if (isStartOfField || i == line.Length - 1)
                    {
                        isQuotes = !isQuotes;
                        isStartOfField = !isStartOfField;
                        continue;
                    }

                    if (isQuotes)
                    {
                        if (quote == line[i + 1])
                        {
                            i++;
                        }
                        else
                        {
                            if (line[i + 1] != delimiter)
                            {
                                throw new ArgumentException("Delimiter expected after closed quotes", "line");
                            }

                            isQuotes = false;
                            continue;
                        }
                    }
                }
                sb.Append(line[i]);
                isStartOfField = false;
            }


            return sb.ToString().Split(escapeDelimiter);
        }

        /// <summary>
        /// Returns object of T by using TypeDescriptor for a string array containing property values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="csvline"></param>
        /// <returns></returns>
        public static T ConvertFromCsv<T>(this string[] csvline)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            
            return (T)(converter.ConvertFrom(csvline));
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        
        public static bool NotEqualTo(this string value, string compareTo)
        {
            return string.Compare(value, compareTo, StringComparison.OrdinalIgnoreCase) != 0;
        }

        public static bool NotEqualToAny(this string value, string[] compareTo)
        {
            return compareTo.All(value.NotEqualTo);
        }
    }
}
