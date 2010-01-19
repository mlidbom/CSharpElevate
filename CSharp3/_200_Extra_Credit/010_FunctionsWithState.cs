using System;
using NUnit.Framework;

namespace CSharp3._200_Extra_Credit
{
    [TestFixture]
    public class FunctionsWithState
    {
        [Test]
        public void MeansFunctionsCanHaveState()
        {
            var counter = CreateCounter();
            for (var i = 1; i <= 5; i++)
            {
                Assert.That(counter(), Is.EqualTo(i));
            }
        }

        private static Func<int> CreateCounter()
        {
            var count = 0;
            return () => ++count;
        }
    }
}