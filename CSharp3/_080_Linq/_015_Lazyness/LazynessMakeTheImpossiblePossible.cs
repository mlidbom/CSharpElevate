using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using CSharp3._090_PrinciplesViaSolid._020_UseAndCreateClosures;
using Void.Linq;

namespace CSharp3._080_Linq._005_Lazyness
{
    /// <summary>
    /// As a general rule Linq logic performs spectacularly well.
    /// This is why:
    /// </summary>
    [TestFixture]
    public class LazynessMakeTheImpossiblePossible
    {
        [Test]
        public void UseIntMaxvalueSquaredIntegersToFindTheFirst100NumbersDivisibleBy5()
        {
            Func<double, long> toExaBytes = i => (long)(i / Math.Pow(1000, 6));
            Math.Pow(int.MaxValue, 2)
                .Transform(toExaBytes)
                .Do(me => Console.WriteLine("Looking for the result in {0} ExaByte of data\n", me));

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

            Console.WriteLine("Results");
            intMaxValueSquaredInLength
                .Where(i => i % 5 == 0)
                .Take(10)
                .ForEach(Console.WriteLine);

            #region print: Searching the data took...

            Console.WriteLine("\nSearching the data took {0} milliseconds\n", watch.ElapsedMilliseconds);

            #endregion

            //JUST DON'T CALLY ANY OPERATORS THAT FORCE ITERATION: 
            //var theReallyLongWait = intMaxValueSquaredInLength.Count(); //Don't do this. It will take "some" time
        }
    }
}