using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp3.Extensions.Hierarchies
{
    //Multiple inheritance of functionality
    public interface IHierarchyDecorator<T> : IHierarchy<IHierarchyDecorator<T>>
    {
        T Decorated { get; }
    }

    public static class HierarchyDecorator
    {
        public static IHierarchyDecorator<T> AsHierarchy<T>(this T me,
                                                            Func<T, IEnumerable<T>> childGetter)
        {
            return new FieldBackedHierarchyDecorator<T>(me, childGetter);
        }

        public static IEnumerable<T> FlattenHierarchy<T>(this T me,
                                                         Func<T, IEnumerable<T>> childGetter)
        {
            return me.AsHierarchy(childGetter) //Decorator
                .FlattenHierarchy() //Hierarchy
                .Undecorate(); //Decorator
        }


        //Hmmmmm, covariance
        public static IEnumerable<T> Undecorate<T>(this IEnumerable<IHierarchyDecorator<T>> root)
        {
            return root.Select(me => me.Decorated);
        }

        //private inner class
        private class FieldBackedHierarchyDecorator<T> : IHierarchyDecorator<T>
        {
            private readonly Func<T, IEnumerable<T>> _childGetter;

            public IEnumerable<IHierarchyDecorator<T>> Children
            {
                get
                {
                    return _childGetter(Decorated).Select(child => child.AsHierarchy(_childGetter));
                }
            }

            public T Decorated { get; private set; }

            public FieldBackedHierarchyDecorator(T nodeValue, Func<T, IEnumerable<T>> childGetter)
            {
                Decorated = nodeValue;
                _childGetter = childGetter;
            }
        }
    }
}