using System;
using System.Collections.Generic;

namespace CSharp3.Support.Linq
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var instance in sequence)
            {
                action(instance);
            }
        }
    }
}