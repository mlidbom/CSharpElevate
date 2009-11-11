using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp3._050_ExtensionMethods
{
    //Class hosting extension methods must be static
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> me, Action<T> action)
        {
            foreach (var value in me)
            {
                action(value);
            }
        }

        public static IEnumerable<TResult> Select<TSource,TResult>(this IEnumerable<TSource> me, Func<TSource, TResult> transform)
        {
            foreach (var value in me)
            {
                yield return transform(value);
            }
        }

        public static IEnumerable<int> Through(this int me, int max)
        {
            while(me <= max)
            {
                yield return me++;
            }
        }
    }

    [TestFixture]
    public class Demo
    {
        [Test]
        public void MakesForSimpleMoreDeclarativeCode()
        {
            //No longer do you need to do this:
            for(int i = 1; i <= 10; i++)
            {
                int squared = i*i;
                Console.WriteLine(squared);
            }

            //Now you can do this:
            foreach (var i in 1.Through(10).Select(x => x*x))
            {
                Console.WriteLine(i);
            }

            //even this.
            1.Through(10).Select(x=>x*x).ForEach(Console.WriteLine);
        }
    }
}
