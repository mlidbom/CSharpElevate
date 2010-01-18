using System.Collections.Generic;
using System.Linq;

namespace CSharp4._060_Examples.Decorator
{
    public static class Decorator
    {        
        public static IEnumerable<T> Undecorate<T>(this IEnumerable<IDecorator<T>> root)
        {
            return root.Select(me => me.Decorated);
        }
    }
}