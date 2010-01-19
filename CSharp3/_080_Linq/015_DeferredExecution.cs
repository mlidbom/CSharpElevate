using System;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._080_Linq
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
                                            return me*me;
                                        };

            var squares = 1.Through(10).Select(square);
            Assert.That(squareCalled, Is.False);

            //ToList as well as any other method that return a concrete type rather than 
            //an interface will cause iteration
            //All operators that return IEnumerable<T> are lazy.
            squares.ToList();
            Assert.That(squareCalled, Is.True);
        }
    }
}