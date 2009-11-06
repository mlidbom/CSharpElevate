using System;
using NUnit.Framework;

namespace CSharp3.LambdaExpressions
{
    [TestFixture]
    public class WithTypeInference
    {
        [Test]
        public void DoesNotWorkWithLambdas()
        {
            var square1 = new Func<int, int>(x => x * x);
            Func<int, int> square2 = x => x * x;
            //var square3 = x => x * x; //"error CS0815: Cannot assign lambda expression to an implicitly-typed local variable"
        }
        
    }
}