using System;
using System.Collections.Generic;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._075_ActionFunc
{
    [TestFixture]
    public class ActionAndFunc
    {
        private delegate bool IntPredicate(int value);

        [Test]
        public void AreGenericTypefForDelegateDeclarations()
        {
            //Func returns a value
            Func<int, int> square = parm => parm*parm;

            //Action is void.
            Action<int> mockSaveFunc = parm => { };

            Action<int, int, int, int> maxParamsAction;
            Func<int, int, int, int, int> maxParamsFunc;
        }

        [Test]
        public void ShouldAlwaysBeUsedBecauseOfCompatibilityIssues()
        {
            IntPredicate largerThan5Predicate = param => param > 5;
            Func<int,bool> largerThan5Func = param => param > 5;

            IEnumerable<int> sixThroughTen;
            // Compile time error. IntPredicate not compatible with Func<int,bool> that Where takes
            //sixThroughTen = 1.Through(10).Where(largerThan5);            
            sixThroughTen = 1.Through(10).Where(largerThan5Func);
            sixThroughTen = 1.Through(10).Where(me => largerThan5Predicate(me));
        }
    }
}