using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace CSharp4._050_Dynamic
{
    [TestFixture]
    public class _010_Dynamic
    {
        [Test]
        public void ThrowsExceptionAtRuntimeWhenCallingNonexistingMethods()
        {
            dynamic myObject = new int();
            Assert.Throws<RuntimeBinderException>(() => myObject.OperationThatDoesNotExist());
        }

        [Test]
        public void ContrastedWith30()
        {
            //3.0 style
            object objectDynamic = MyDynamic.GetInstance();//Com or DLR object...
            Type type = objectDynamic.GetType();
            object result = type.InvokeMember("Return",
                     BindingFlags.InvokeMethod,
                     null,
                     objectDynamic,
                     new object[] { 1 });
            Assert.That(Convert.ToInt32(result), Is.EqualTo(1));

            //4.0 style
            dynamic myDynamic = MyDynamic.GetInstance();
            Assert.That(myDynamic.Return(1), Is.EqualTo(1));
        }

        #region HasFullCSharpSemantics

        #region calls to

        class MyDynamic
        {
            public int Return(int value) { return value; }
            public string Return(string value) { return value; }
            public object Return(object value) { return value; }

            public string Format(object value1 = null, object value2 = null, object value3 = null)
            {
                return string.Format("{0},{1},{2}", value1, value2, value3);
            }

            private MyDynamic(){}

            public static object GetInstance()
            {
                return new MyDynamic();
            }
        }

        [Test]
        public void CallsToDynamicResolvesAsWithStaticType()
        {
            dynamic myDynamic = MyDynamic.GetInstance();
            Assert.That(myDynamic.Return(1), Is.EqualTo(1));
            Assert.That(myDynamic.Return("hi"), Is.EqualTo("hi"));
            Assert.That(myDynamic.Return(myDynamic), Is.EqualTo(myDynamic));
            Assert.That(myDynamic.Format(value2: "value2"), Is.EqualTo(",value2,"));
        }

        #endregion

        #region calls with

        private static int Return(int value) { return value; }
        private static string Return(string value) { return value; }
        private static object Return(object value) { return value; }
        public static string Format(object value1 = null, object value2 = null, object value3 = null)
        {
            return string.Format("{0},{1},{2}", value1, value2, value3);
        }

        [Test]
        public void CallsWithDynamicResolveAsWithStaticType()
        {
            dynamic dynamic1 = 1;
            dynamic dynamicHi = "hi";
            dynamic dynamicObject = new object();
            dynamic dynamicValue2 = "value2";

            //Calls with dynamic type resolve as with static types.
            Assert.That(Return(dynamic1), Is.EqualTo(1));
            Assert.That(Return(dynamicHi), Is.EqualTo("hi"));
            Assert.That(Return(dynamicObject), Is.EqualTo(dynamicObject));
            Assert.That(Format(value2: dynamicValue2), Is.EqualTo(",value2,"));
        }

        #endregion

        #endregion

    }    
}