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
    /// </summary>
    [TestFixture]
    public class Closures
    {
        [Test]
        public void CapturesVariablesNotValues()
        {
            int theInt = 0;
            //You can use variables in context within the lambda
            Func<int> increment = () => ++theInt; 
            
            //You have only given a name to a function,
            //not executed anything.
            Assert.That(theInt, Is.EqualTo(0));


            Assert.That(increment(), Is.EqualTo(1));
            
            //the original variable is changed.
            Assert.That(theInt, Is.EqualTo(1));
        }
    }
}