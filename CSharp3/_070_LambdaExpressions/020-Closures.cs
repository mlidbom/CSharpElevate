using System;
using NUnit.Framework;

namespace CSharp3._070_LambdaExpressions
{
    /// <summary>
    /// A lambda captures any variables used in its body.
    /// What this means is that whenever the lambda is 
    /// executed, any access of variables not declared
    /// within the lambda will bind to the external variable
    /// meaning that the lambda cat "see" state modified
    /// elsewhere, and also cause modifications
    /// to state outside of the lambda.
    /// 
    /// Additionally the lifetime of captured variables is extended to that of the lambda.
    /// </summary>
    [TestFixture]
    public class Closures
    {
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

        [Test]
        public void MeansFunctionsCanHaveState()
        {
            var counter = CreateCounter();
            for (int i = 1; i <= 5; i++)
            {
                Assert.That(counter(), Is.EqualTo(i));
            }
        }

        private static Func<int> CreateCounter()
        {
            int count = 0;
            return () => ++count;
        }
    }
}