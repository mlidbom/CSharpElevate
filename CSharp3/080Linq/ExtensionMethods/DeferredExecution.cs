using System;
using System.Linq;
using NUnit.Framework;

namespace CSharp3.Linq.ExtensionMethods
{
    [TestFixture]
    public class DeferredExecution
    {
        [Test]
        public void ShouldDeferrExecutionUntilIteration()
        {
            bool called = false;
            Func<int, int> squareAndMarkAsCalled = me =>
                                                       {
                                                           called = true;
                                                           return me*me;
                                                       };

            var squares = Enumerable.Range(0, 9).Select(squareAndMarkAsCalled);
            Assert.That(called, Is.False);

            var squaresList = squares.ToList();
            Assert.That(called, Is.True);
        }
    }
}