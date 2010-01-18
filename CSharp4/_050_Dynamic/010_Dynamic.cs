using System;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace CSharp4._050_Dynamic
{
    [TestFixture]
    public class _010_Dynamic
    {
        class MyDynamic
        {
            public int Return(int value) { return value; }
            public string  Return(string value) { return value; }
            public object Return(object value) { return value; }

            public string Format(object value1 = null, object value2 = null, object value3 = null)
            {
                return string.Format("{0},{1},{2}", value1, value2, value3);   
            }
        }

        [Test]
        public void ThrowsExceptionAtRuntimeWhenCallingNonexistingMethods()
        {
            dynamic myObject = new MyDynamic();
            Assert.Throws<RuntimeBinderException>(() => myObject.OperationThatDoesNotExist());
        }       

        [Test]
        public void HasFullCSharpSemantics()
        {
            dynamic myObject = new MyDynamic();
            //Calls to dynamic type resolve as with static types.
            Assert.That(myObject.Return(1), Is.EqualTo(1));
            Assert.That(myObject.Return("hi"), Is.EqualTo("hi"));
            Assert.That(myObject.Return(myObject), Is.EqualTo(myObject));
            Assert.That(myObject.Format(value2: "value2"), Is.EqualTo(",value2,"));

            dynamic dynamic1 = 1;
            dynamic dynamicHi = "hi";
            dynamic dynamicObject = new object();
            dynamic dynamicValue2 = "value2";

            Assert.That(Return(dynamic1), Is.EqualTo(1));
            Assert.That(Return(dynamicHi), Is.EqualTo("hi"));
            Assert.That(Return(dynamicObject), Is.EqualTo(dynamicObject));
            Assert.That(Format(value2:dynamicValue2), Is.EqualTo(",value2,"));

            //Calls with dynamic type resolve as with static types.
        }

        private static int Return(int value) { return value; }
        private static string Return(string value) { return value; }
        private static object Return(object value) { return value; }
        public static string Format(object value1 = null, object value2 = null, object value3 = null)
        {
            return string.Format("{0},{1},{2}", value1, value2, value3);
        }
    }    
}