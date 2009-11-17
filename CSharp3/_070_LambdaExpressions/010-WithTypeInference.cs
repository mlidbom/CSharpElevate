using System;
using NUnit.Framework;
using Void.Linq;
using System.Linq;

namespace CSharp3._070_LambdaExpressions
{
    [TestFixture]
    public class WithTypeInference
    {
        [Test]
        public void DoesNotWorkWithLambdasAndVaribles()
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
            var twelweThrough14 = 12.Through(20).Where(me => me < 15);
            Assert.That(twelweThrough14, Is.EqualTo(12.Through(14)));          
        }
    }
}