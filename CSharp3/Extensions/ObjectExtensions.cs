using System.Collections.Generic;

namespace CSharp3.Util
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
    }
}