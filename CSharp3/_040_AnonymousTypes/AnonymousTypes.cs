using System;
using NUnit.Framework;

namespace CSharp3._040_AnonymousTypes
{
    [TestFixture]
    public class AnonymousTypes
    {
        [Test]
        public void ProvideTypeSafeAccessToPropertiesNeverDeclared()
        {
            var person = new {ForeName = "Magnus", SurName = "Lidbom"};

            Console.WriteLine("{0} {1}", person.ForeName, person.SurName);
            //error CS0200: Property or indexer 'AnonymousType#1.ForeName' cannot be assigned to -- it is read only
            //person.ForeName = 23; 
        }
    }
}