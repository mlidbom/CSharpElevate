using System;
using NUnit.Framework;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class AreClosures
    {
        [Test]
        public void SimpleUsage()
        {
            Func<int, int> square = param => param*param;
            Assert.That(square(2), Is.EqualTo(4));
        }

        [Test]
        public void CapturesVariablesNotValues()
        {
            var theInt = 0;
            //You can use variables in context within the lambda
            Func<int> increment = () => ++theInt;

            //You have only given a name to a function,
            //not executed anything.
            Assert.That(theInt, Is.EqualTo(0));


            Assert.That(increment(), Is.EqualTo(1));

            //the original variable is changed.
            Assert.That(theInt, Is.EqualTo(1));
        }

        [Test]
        public void ExtendsLifetimeOfCapturedVariables()
        {
            var get5 = CreateIntGetter(5);
            Assert.That(get5(), Is.EqualTo(5));
        }

        private static Func<int> CreateIntGetter(int i)
        {
            return () => i;
        }    
    }
}