using System;
using System.Collections.Generic;
using System.Linq;
using CSharp4._020_GenericVariance.Examples;

namespace CSharp3.Extensions.Hierarchies
{
    public interface IAutoHierarchy<T> : IHierarchy<IAutoHierarchy<T>>, IWrapper<T>
    {
    }

    public static class Hierarchy
    {
        private class AutoHierarchy<T> : IAutoHierarchy<T>
        {
            private readonly Func<T, IEnumerable<T>> _childGetter;

            public IEnumerable<IAutoHierarchy<T>> Children
            {
                get { return _childGetter(Wrapped).Select(child => child.AsHierarchy(_childGetter)); }
            }

            public T Wrapped { get; private set; }

            public AutoHierarchy(T nodeValue, Func<T, IEnumerable<T>> childGetter)
            {
                Wrapped = nodeValue;
                _childGetter = childGetter;
            }
        }

        public static IAutoHierarchy<T> AsHierarchy<T>(this T me, Func<T, IEnumerable<T>> childGetter)
        {
            return new AutoHierarchy<T>(me, childGetter);
        }

        public static IEnumerable<T> Flatten<T>(this T root) where T : IHierarchy<T>
        {
            return new[]{root}.FlattenHierarchy(me => me.Children);
        }

        //No longer needed now that we have covariance!
        //public static IEnumerable<T> Unwrap<T>(this IEnumerable<IAutoHierarchy<T>> root)
        //{
        //    return root.Select(me => me.Wrapped);
        //}

        public static IEnumerable<TSource> FlattenHierarchy<TSource>(this IEnumerable<TSource> source,
                                                                     Func<TSource, IEnumerable<TSource>> childrenSelector)
        {
            foreach (var item in source)
            {
                foreach (var child in FlattenHierarchy(childrenSelector(item), childrenSelector))
                {
                    yield return child;
                }
                yield return item;
            }
        }
    }
}