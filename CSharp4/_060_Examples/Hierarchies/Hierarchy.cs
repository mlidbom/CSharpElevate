using System.Collections.Generic;
using System.Linq;

namespace CSharp4._060_Examples.Hierarchies
{
    public interface IHierarchy<out T> where T : IHierarchy<T>
    {
        IEnumerable<T> Children { get; }
    }

    public static class Hierarchy
    {
        public static IEnumerable<T> FlattenHierarchy<T>(this T root) where T : IHierarchy<T>
        {
            return Seq.New(root).FlattenHierarchy();
        }

        public static IEnumerable<TSource> FlattenHierarchy<TSource>(
            this IEnumerable<TSource> roots) where TSource : IHierarchy<TSource>
        {
            return roots
                .SelectMany(root => root.Children.FlattenHierarchy())
                .Concat(roots);
        }
    }

    public static class Seq
    {
        public static IEnumerable<T> New<T>(params T[] values)
        {
            return values;
        }
    }
}