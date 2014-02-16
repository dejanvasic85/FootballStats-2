using System;
using System.Collections.Generic;

namespace Football
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes the required action against every item in the collection
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (action == null)
                throw new ArgumentNullException("action");

            foreach (T item in collection)
            {
                action(item);
            }
        }
    }
}