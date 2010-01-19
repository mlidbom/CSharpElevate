using System;
using System.Collections.Generic;
using System.Linq;
using CSharp4._060_Examples.Decorator;

namespace CSharp4._060_Examples.Hierarchies
{    
    public interface IHierarchyDecorator<out T> : IHierarchy<IHierarchyDecorator<T>>, IDecorator<T> {}

    public static class HierarchyDecorator
    {
        public static IHierarchyDecorator<T> AsHierarchy<T>(this T me, Func<T, IEnumerable<T>> childGetter)
        {
            return new FiealdBackedHierarchyDecorator<T>(me, childGetter);
        }

        public static IEnumerable<T> FlattenHierarchy<T>(this T me, Func<T, IEnumerable<T>> childGetter)
        {
            return me.AsHierarchy(childGetter) //Decorator
                .FlattenHierarchy() //Hierarchy
                .Undecorate(); //Decorator
        }


        //Can be implemented by Decorator now that we have covariance
        //public static IEnumerable<T> Undecorate<T>(this IEnumerable<IHierarchyDecorator<T>> root)
        //{
        //    return root.Select(me => me.Decorated);
        //}

        //private inner class...
        private class FiealdBackedHierarchyDecorator<T> : IHierarchyDecorator<T>
        {
            private readonly Func<T, IEnumerable<T>> _childGetter;

            public IEnumerable<IHierarchyDecorator<T>> Children
            {
                get { return _childGetter(Decorated).Select(child => child.AsHierarchy(_childGetter)); }
            }

            public T Decorated { get; private set; }

            public FiealdBackedHierarchyDecorator(T nodeValue, Func<T, IEnumerable<T>> childGetter)
            {
                Decorated = nodeValue;
                _childGetter = childGetter;
            }
        }
    }
}