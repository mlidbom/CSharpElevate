using System;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using CSharp3.Extensions.Linq;
using NUnit.Framework;

namespace CSharp3._100_PuttingItAllTogether
{
    [TestFixture]
    public class TestingNumbersForBeingPrime
    {
        [Test]
        public void FindAndPrintAllPrimesBelow100()
        {
            1.Through(99)
                .Where(number => number.IsPrime())
                .ForEach(Console.WriteLine);
        }
    }

    public static class Int32Ex
    {
        public static bool IsPrime(this int candidate)
        {
            //As promised, testing for primes in a single row
            return candidate > 1 && 2.Through(candidate - 1).None(divisor => candidate.IsDivisibleBy(divisor));
        }

        public static bool IsDivisibleBy(this int me, int divisor)
        {
            return me%divisor == 0;
        }
    }
}