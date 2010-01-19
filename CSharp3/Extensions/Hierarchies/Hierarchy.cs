using System.Collections.Generic;
using System.Linq;
using CSharp3.Extensions.Linq;

namespace CSharp3.Extensions.Hierarchies
{
    public interface IHierarchy<T> where T : IHierarchy<T>
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
}