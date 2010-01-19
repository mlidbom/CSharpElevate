using System.Collections.Generic;

namespace CSharp4._060_Examples.Linq
{
    public static class Seq
    {
        public static IEnumerable<T> New<T>(params T[] values)
        {
            return values;
        }
    }
}