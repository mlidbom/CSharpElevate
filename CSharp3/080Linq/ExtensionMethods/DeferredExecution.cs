using System;
using System.Collections.Generic;
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
            bool squareCalled = false;
            Func<int, int> square = me =>
                                        {
                                            squareCalled = true;
                                            return me*me;
                                        };

            IEnumerable<int> squares = Enumerable.Range(0, 9).Select(square);
            Assert.That(squareCalled, Is.False);

            ///ToList as well as any other method that return a concrete type rather than 
            /// IEnumerable<T> or  IQueryable<T> will cause iteration.
            squares.ToList();
            Assert.That(squareCalled, Is.True);
        }
    }
}