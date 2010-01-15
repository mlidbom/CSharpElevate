using System;
using System.Collections.Generic;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using CSharp3.Util.Linq;
using NUnit.Framework;
using CSharp3.Util;

namespace CSharp3._200_MindBenders
{
    [TestFixture]
    public class _005_LinqTricks
    {
        #region Level 1

        [Test]
        public void FindAndPrintAllPrimesBelow100()
        {
            1.Through(99)
                .Where(i => 2.Through(i - 1).None(candidate => i % candidate == 0))
                .ForEach(Console.WriteLine);
        }

        #endregion

        #region Level 2

        [Test]
        public void FindAllPairsOfIntsIAndJWhereJIsSmallerThanIAndISmallerThan100WhereTheSumOfThePairsIsAPrimeNumberAndPrintThem()
        {
            1.Through(99)
                .SelectMany(i => i.Repeat(i - 1).Zip(1.Through(i - 1)))
                .Where(i => 2.Through(i.First - 1).None(candidate => (i.First + i.Second) % candidate == 0))
                .ForEach(pair => Console.WriteLine("Pair {0} has sum {1}. {1} is prime", pair, pair.First + pair.Second));
        }

        #endregion

        #region How it works

        // the  % operator returns 0 if the first operand is evenly divisible by the second
        private static readonly Func<int, int, bool> IsDivisibleBy = (number, test) => number % test == 0;

        //if i is divisible by any number it is not prime
        private static readonly Func<int, bool> IsPrime = possiblePrime => 2.Through(possiblePrime - 1).None(divisor => IsDivisibleBy(possiblePrime, divisor));

        [Test]
        public void FindAndPrintAllPrimesBelow100Explained()
        {
            1.Through(99)
                .Where(IsPrime)
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void FindAllPairsOfIntsIAndJWhereJIsSmallerThanIAndISmallerThan100WhereTheSumOfThePairsIsAPrimeNumberAndPrintThemExplained()
        {
            Func<int, IEnumerable<Zipping.Pair<int, int>>> generatePairsWithNumbersLowerThan =
                i => i.Repeat(i - 1) //Repeat i (i - 1) times
                         .Zip( //Pair each i with 
                         1.Through(i - 1) //A number between 1 and i
                         );
            1.Through(99)
                .SelectMany(generatePairsWithNumbersLowerThan)
                .Where(pair => IsPrime(pair.First + pair.Second))
                .ForEach(pair => Console.WriteLine("Pair {0} has sum {1}. {1} is prime", pair, pair.First + pair.Second));
        }

        #endregion
    }
}