using System;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace CSharp4._050_Dynamic
{
    [TestFixture]
    public class _010_Dynamic
    {
        class Printer
        {
            public string Print(int value) { return "int"; }
            public string Print(string value) { return "string"; }
            public string Print(object value) { return "object"; }

            public string Print(object value1 = null, object value2 = null, object value3 = null)
            {
                return string.Format("{0},{1},{2}", value1, value2, value3);   
            }
        }

        [Test]
        public void ThrowsExceptionAtRuntimeWhenCallingNonexistingMethods()
        {
            dynamic myObject = new Printer();
            Assert.Throws<RuntimeBinderException>(() => myObject.OperationThatDoesNotExist());
        }       

        [Test]
        public void HasFullCSharpSemantics()
        {
            dynamic myObject = new Printer();
            Assert.That(myObject.Print(1), Is.EqualTo("int"));
            Assert.That(myObject.Print(""), Is.EqualTo("string"));
            Assert.That(myObject.Print(new object()), Is.EqualTo("object"));
            Assert.That(myObject.Print(value2: "value1"), Is.EqualTo(",value1,"));
        }
    }    
}