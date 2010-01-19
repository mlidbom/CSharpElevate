using System;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using CSharp3.Extensions.Linq;
using NUnit.Framework;

namespace CSharp3._200_Extra_Credit
{
    [TestFixture]
    public class _005_LinqTricks
    {
        [Test]
        public void FindAndPrintAllPrimesBelow100()
        {
            2.Through(99)
                .Where(
                possiblePrime =>
                2.Through(possiblePrime - 1).None(divisor => possiblePrime%divisor == 0))
                .ForEach(Console.WriteLine);
        }

        #region how it works      

        [Test]
        public void FindAndPrintAllPrimesBelow100Explained()
        {
            Func<int, int, bool> isDivisibleBy = (number, test) => number%test == 0;
            Func<int, bool> isPrime = possiblePrime => 2.Through(possiblePrime - 1)
                                                           .None(
                                                           divisor =>
                                                           isDivisibleBy(possiblePrime, divisor));

            1.Through(99)
                .Where(isPrime)
                .ForEach(Console.WriteLine);
        }

        #endregion
    }
}