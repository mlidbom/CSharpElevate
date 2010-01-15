using System.Collections.Generic;
using System.Linq;
using CSharp3.Extensions.Hierarchies;

namespace CSharp4._020_GenericVariance.Examples
{
    public interface IWrapper<T>
    {
        T Wrapped { get; }
    }

    public static class Wrapper
    {
        public static IEnumerable<T> Unwrap<T>(this IEnumerable<IWrapper<T>> root)
        {
            return root.Select(me => me.Wrapped);
        }
    }
}