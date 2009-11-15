using System;
using NUnit.Framework;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class WithTypeInference
    {
        [Test]
        public void DoesNotWorkWithLambdas()
        {
            var square1 = new Func<int, int>(x => x * x);
            Assert.That(square1(2), Is.EqualTo(4));

            Func<int, int> square2 = x => x * x;
            Assert.That(square2(2), Is.EqualTo(4));

            //var square3 = x => x * x; //"error CS0815: Cannot assign lambda expression to an implicitly-typed local variable"                       
        }
        
    }
}