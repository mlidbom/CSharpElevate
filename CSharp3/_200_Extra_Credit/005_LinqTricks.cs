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
        [Test]
        public void FindAndPrintAllPrimesBelow100()
        {
            2.Through(99)
                .Where(possiblePrime => 2.Through(possiblePrime - 1).None(divisor => possiblePrime % divisor == 0))
                .ForEach(Console.WriteLine);
        }


        #region how it works      

        [Test]
        public void FindAndPrintAllPrimesBelow100Explained()
        {
            Func<int, int, bool> IsDivisibleBy = (number, test) => number % test == 0;
            Func<int, bool> IsPrime = possiblePrime => 2.Through(possiblePrime - 1).None(divisor => IsDivisibleBy(possiblePrime, divisor));

            1.Through(99)
                .Where(IsPrime)
                .ForEach(Console.WriteLine);
        }


        #endregion  
    }
}