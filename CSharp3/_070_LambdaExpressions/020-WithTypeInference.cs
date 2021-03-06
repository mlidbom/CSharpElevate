using System;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class TypeInference
    {
        [Test]
        public void DoesNotWorkWithLambdasAndVariables()
        {
            var square1 = new Func<int, int>(x => x*x);
            Assert.That(square1(2), Is.EqualTo(4));

            Func<int, int> square2 = x => x*x;
            Assert.That(square2(2), Is.EqualTo(4));
            //"error CS0815: Cannot assign lambda expression to an implicitly-typed local variable"
            //var square3 = x => x * x; 
        }


        [Test]
        public void DoesWorkWithParameters()
        {
            var twelweThrough14 = 12.Through(20)
                                    .Where(me => me < 15);

            Assert.That(twelweThrough14,
                        Is.EqualTo(12.Through(14)));
        }
    }
}