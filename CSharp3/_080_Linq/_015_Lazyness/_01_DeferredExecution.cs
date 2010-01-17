using System;
using System.Linq;
using NUnit.Framework;
using CSharp3._050_ExtensionMethods;

namespace CSharp3._080_Linq._015_Lazyness
{
    [TestFixture]
    public class DeferredExecution
    {
        [Test]
        public void ShouldDeferrExecutionUntilIteration()
        {
            var squareCalled = false;
            Func<int, int> square = me =>
                                        {
                                            squareCalled = true;
                                            return me * me;
                                        };

            var squares = 1.Through(10).Select(square);
            Assert.That(squareCalled, Is.False);

            //ToList as well as any other method that return a concrete type rather than 
            // IEnumerable<T> or  IQueryable<T> will cause iteration.
            squares.ToList();
            Assert.That(squareCalled, Is.True);
        }
    }
}