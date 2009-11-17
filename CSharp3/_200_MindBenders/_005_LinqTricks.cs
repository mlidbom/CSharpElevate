using System;
using System.Linq;
using Void.Linq;
using NUnit.Framework;

namespace CSharp3._200_MindBenders
{
    [TestFixture]
    public class _005_LinqTricks
    {
        [Test]
        public void FindAllPairsOfIntsIAndJWhereJIsSmallerThanIAndISmallerThan100WhereTheSumOfThePairsIsAPrimeNumber()
        {
            Func<int, int, bool> isDivisibleBy = (number, test) => number % test == 0;
            Func<int, bool> isPrime = i => !2.Through(i - 1).Any(number => isDivisibleBy( i, number));

            1.Through(99)
                .SelectMany(i => i.Times(i - 1).Zip(1.Through(i - 1)))
                .Where(pair => isPrime(pair.First + pair.Second))
                .ForEach(pair => Console.WriteLine("Pair {0} has sum {1}. {1} is prime", pair, pair.First + pair.Second));

        }
    }
}