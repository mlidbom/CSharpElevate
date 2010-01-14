using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp4._020_GenericVariance
{
    [TestFixture]
    public class AppliesOnlyToInterfacesAndDelegates
    {
        [Test]
        public void CoVarianceAllowsAssigningDerivedGenericInstancesToBaseGenericInterfaces()
        {            
            IEnumerable<string> strings = new List<string> { "aoeu", "aoeu", "aeou" };
            IEnumerable<object> objects = strings; //This would not compile in C#3
        }

        [Test]
        public void ContraVarianceAllowsAssigningBaseGenericInstancesDerivedGenericInterfaces()
        {
            Action<object> printObject = toPrint => Console.WriteLine(toPrint);
            Action<string> printString = printObject;//This would not compile in C#3
        }
    }
}