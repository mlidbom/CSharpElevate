using System;
using NUnit.Framework;

namespace CSharp4._030_Parameters
{
    [TestFixture]
    public class _020_Named
    {
        [Test]
        public void MakesCallsMoreReadable()
        {
            new DateTime(year: 1, month: 2, day: 3);
        }

        [Test]
        public void EnablesReorderingOfArguments()
        {
            new DateTime(day: 3, month: 2, year: 1);
        }
    }
}