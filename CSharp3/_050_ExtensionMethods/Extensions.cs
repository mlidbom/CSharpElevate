using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp3._050_ExtensionMethods
{
    //Class hosting extension methods must be static
    public static class Extensions
    {
        ///the "this" keyword marks a parameter as being the equivalent 
        ///of the this reference in a member method.
        public static void ForEach<T>(this IEnumerable<T> me, Action<T> action)
        {
            foreach (var value in me)
            {
                action(value);
            }
        }

        public static IEnumerable<int> Through(this int me, int max)
        {
            while (me <= max)
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
            for (var i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
            }

            //Now you can do this:
            1.Through(10).ForEach(Console.WriteLine);
        }
    }
}