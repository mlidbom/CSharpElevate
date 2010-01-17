using System;
using System.Collections.Generic;

namespace CSharp3.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<T> Repeat<T>(this T me, int times)
        {
            while (times-- > 0)
            {
                yield return me;
            }
        }

        /// <summary>
        /// Applies a transformation to any object. 
        /// </summary>
        public static TReturn Transform<TSource, TReturn>(this TSource me, Func<TSource, TReturn> transform)
        {
            return transform(me);
        }
    }
}