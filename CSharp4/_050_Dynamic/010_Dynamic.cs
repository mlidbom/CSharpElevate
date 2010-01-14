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
        }

        [Test]
        public void ThrowsExceptionAtRuntimeWhenCallingNonexistingMethods()
        {
            dynamic myObject = new Printer();
            Assert.Throws<RuntimeBinderException>(() => myObject.OperationThatDoesNotExist());
        }       

        [Test]
        public void HasFullCSharpOverLoadResolutionSemantics()
        {
            dynamic myObject = new Printer();
            Assert.That(myObject.Print(1), Is.EqualTo("int"));
            Assert.That(myObject.Print(""), Is.EqualTo("string"));
            Assert.That(myObject.Print(new object()), Is.EqualTo("object"));
        }
    }    
}