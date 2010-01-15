using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp3.Util.Linq
{
    public static class Seq
    {
        public static IEnumerable<T> New<T>(params T[] elements)
        {
            return elements;
        }


        public static bool None<T>(this IEnumerable<T> me, Func<T, bool> predicate)
        {
            return !me.Any(predicate);
        }
    }
}