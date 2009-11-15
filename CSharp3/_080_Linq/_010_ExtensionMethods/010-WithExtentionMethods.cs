using System;
using System.Linq;
using NUnit.Framework;
using CSharp3.Support.Linq;

namespace CSharp3._080_Linq._010_ExtensionMethods
{
    [TestFixture]
    public class WithExtentionMethods
    {
        [Test]
        public void WhereFiltersStuffForYou()
        {
            var odds = Enumerable.Range(1, 10).Where(number => number%2 != 0);
            odds.ForEach(Console.WriteLine);
        }

        [Test]
        public void SelectTransformsSequences()
        {
            var squaresFrom1To10 = Enumerable.Range(1, 10).Select(x => x * x);
            squaresFrom1To10.ForEach(Console.WriteLine);
        }

        [Test]
        public void EnablesDifferentWaysOfDoingThings()
        {
            Func<int, int> square = x => x * x;
            var sumOfSquares = Enumerable.Range(0, 10).Select(square).Sum();
            Console.WriteLine(sumOfSquares);
        }
    }
}