using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CSharp3._050_ExtensionMethods
{
    //Class hosting extension methods must be static
    public static class Extensions
    {
        //the "this" keyword marks a parameter as being the equivalent of the this reference in a normal member method.
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
            Func<int, int> square = x => x * x;


            //No longer do you need to do this:
            for (var i = 1; i <= 10; i++)
            {
                var squared = square(i);
                Console.WriteLine(squared);
            }

            //Now you can do this:
            1.Through(10).Select(square).ForEach(Console.WriteLine);
        }
    }
}