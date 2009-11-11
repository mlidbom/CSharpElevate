using System;
using NUnit.Framework;

namespace CSharp3.AnonymousTypes
{
    [TestFixture]
    public class AnonymousTypes
    {
        [Test]
        public void ProvideTypeSafeAccessToPropertiesNeverDeclared()
        {
            var person = new {ForeName = "Magnus", SurName = "Lidbom"};

            //person.ForeName = 23; //error CS0200: Property or indexer 'AnonymousType#1.ForeName' cannot be assigned to -- it is read only

            Console.WriteLine("{0} {1}", person.ForeName, person.SurName);
        }
    }
}