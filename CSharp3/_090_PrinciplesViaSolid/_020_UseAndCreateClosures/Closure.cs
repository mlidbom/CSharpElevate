using System;
using System.Collections.Generic;
using NUnit.Framework;
using Void.Linq;
using System.Linq;

namespace CSharp3._090_PrinciplesViaSolid._020_UseAndCreateClosures
{
    /// <summary>
    /// A closure in math is different from the meaning of closure in programming.
    /// In math a set Y is said to be closed over an operation X 
    /// if the operation X applied to any member of Y results is a member of Y
    /// 
    /// Why is the mathematical idea of closure important? Because it 
    /// makes building immensely complex logic a simple case of combining
    /// existing operations and members of the set. Linq is so hugely powerful because the
    /// set of all IEnumerable instances is closed over most of the Linq operations
    /// since they take ienumerables and return ienumerables
    /// consider: Select, Where, Join, SelectMany etc
    /// 
    /// Once you have that property creating powerful abstractions
    /// becomes almost trivial because whatever you create gets the 
    /// ability to be easily combined with what already exists for free.
    /// 
    /// 
    /// Whenever you design an abstraction, look for the opportunity 
    /// to close it over operations.
    /// </summary>
    [TestFixture]
    public class Closure
    {
        [Test]
        public void YourOperationIsEasylyComposableWithAllTheOtherClosedOperationsOnTheSet()
        {
            //ConsecutivePairs becomes an int
            Enumerable.Range(1,10).ConsecutivePairs().ForEach(Console.WriteLine);
        }
    }

    public static class LinqExtensionsClosedOverTheSetOfIEnumerable
    {
        /// <summary>
        /// A closed operation of the set of IEnumerables that
        /// return a new IEnumerable that is an IEnumerable of the 
        /// objects in the first IEnuerable paired consecutively
        /// </summary>
        public static IEnumerable<Zipping.Pair<T, T>> ConsecutivePairs<T>(this IEnumerable<T> me)
        {
            // I can easily make use of existing operations of the set
            // Skip is provided my Microsoft, I built Zip, but they 
            //play together completely seamlessly.
            return me.Zip(me.Skip(1));
        }
    }
}