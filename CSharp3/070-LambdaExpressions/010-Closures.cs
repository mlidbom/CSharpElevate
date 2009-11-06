using System;
using NUnit.Framework;

namespace CSharp3.LambdaExpressions
{
    [TestFixture]
    public class Closures
    {
        [Test]
        public void CapturesVariablesNotValues()
        {
            int theInt = 0;
            Func<int> increment = () => ++theInt; //You can use variables in context within the lambda
            
            Assert.That(theInt, Is.EqualTo(0));
            Assert.That(increment(), Is.EqualTo(1));
            Assert.That(theInt, Is.EqualTo(1));//It's not the value that is captured, it's the variable
        }
    }
}