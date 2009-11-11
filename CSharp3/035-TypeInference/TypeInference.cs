using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using CSharp3.Support.Linq;

namespace CSharp3.TypeInference
{
    [TestFixture]
    public class TypeInference
    {
        public void SavesLotsOfTyping()
        {
            //The type is right there. Why would you want to type it again?
            var typeAndIdInstanceLookup = new Dictionary<Type, Func<object, object>>();
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

        [Test]
        public void ImprovesCodeAgility()
        {
            //Comment the first getInts method and uncomment the second. Everything still works without any changes to the code.
            //Func<List<int>> getInts = () => new List<int>{1,2,3,4,5,6,7,8,9};
            Func<IEnumerable<int>> getInts = () => Enumerable.Range(1,9);

            var myInts = getInts();
            myInts.ForEach(Console.WriteLine);
        }
    }
}
