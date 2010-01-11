using System;
using System.Linq;
using NUnit.Framework;
using Void.Linq;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class TypeInference
    {
        [Test]
        public void DoesNotWorkWithLambdasAndVariables()
        {
            var square1 = new Func<int, int>(x => x * x);
            Assert.That(square1(2), Is.EqualTo(4));

            Func<int, int> square2 = x => x * x;
            Assert.That(square2(2), Is.EqualTo(4));

            //var square3 = x => x * x; //"error CS0815: Cannot assign lambda expression to an implicitly-typed local variable"                       
        }


        [Test]
        public void DoesWorkWithParameters()
        {
            var twelweThrough14 = 12.Through(20).Where(me => me < 15/*you can use lambdas as parameters*/);
            Assert.That(twelweThrough14, Is.EqualTo(12.Through(14)));
        }
    }
}