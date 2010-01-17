using System;
using System.Diagnostics;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._200_Extra_Credit
{
    /// <summary>
    /// As a general rule Linq logic performs spectacularly well.
    /// This is why:
    /// </summary>
    [TestFixture]
    public class _02_LazynessMakesTheImpossiblePossible
    {
        [Test]
        public void UseIntMaxvalueSquaredIntegersToFindTheFirst10NumbersDivisibleBy5()
        {
            Console.WriteLine("Facebook has just over 1.5 petabytes of users' photos stored,\ntranslating into roughly 10 billion photos\n");

            Func<double, long> toPetaBytes = i => (long) (i / Math.Pow(10, 15));
            Math.Pow(int.MaxValue, 2)
                .Transform(toPetaBytes)
                .Do(me => Console.WriteLine("Generating {0} petabytes of lazy data...\n", me));

            #region start timing

            var watch = new Stopwatch();
            watch.Start();

            #endregion

            var intMaxValueSquaredInLength = 1.Through(int.MaxValue)
                .SelectMany(num =>
                            1.Through(int.MaxValue));

            #region print: Creating the data took...

            Console.WriteLine("Creating the data took {0} milliseconds\n", watch.ElapsedMilliseconds);
            watch.Reset();
            watch.Start();

            #endregion

            Console.WriteLine("Searching for first 10 numbers divisible by 5...\n");

            intMaxValueSquaredInLength
                .Where(i => i % 5 == 0)
                .Take(10)
                .ForEach(Console.WriteLine);

            #region print: Searching the data took...

            Console.WriteLine("\nSearching the data took {0} milliseconds\n", watch.ElapsedMilliseconds);

            #endregion

            //JUST DON'T CALLY ANY OPERATORS THAT FORCE ITERATION: 
            //var theReallyLongWait = intMaxValueSquaredInLength.Count(); //Don't do this. It will take "some time"
        }
    }
}