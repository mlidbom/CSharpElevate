using System;
using System.Linq;
using NUnit.Framework;
using CSharp3._090_PrinciplesViaSolid._020_UseAndCreateClosures;
using Void.Linq;

namespace CSharp3._080_Linq._005_Lazyness
{
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
            
            var intMaxValueSquaredInLength = Enumerable.Range(1, int.MaxValue)
                                                       .SelectMany(num => 
                                                           Enumerable.Range(1, int.MaxValue));

            Console.WriteLine("Results");
            intMaxValueSquaredInLength
                .Where(i => i % 5 == 0)
                .Take(10)
                .ForEach(Console.WriteLine);
        }
    }
}