using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp3._035_TypeInference
{
    [TestFixture]
    public class TypeInference
    {
        [Test]
        public void SavesLotsOfTyping()
        {
            //The type is right there. Why would you want to type it again?
            var typeAndIdInstanceLookup = new Dictionary<Type, Func<object, object>>();
            Assert.That(typeAndIdInstanceLookup, Is.TypeOf<Dictionary<Type, Func<object, object>>>());
        }

        [Test]
        public void IsStronglyTyped()
        {
            var int10 = 10;
            var double10 = 10.0;

            Assert.That(int10, Is.TypeOf<int>());
            Assert.That(double10, Is.TypeOf<double>());
            Assert.That(int10, Is.Not.TypeOf<double>());

            //int10 = double10; //Compilation error.
        }
    }
}