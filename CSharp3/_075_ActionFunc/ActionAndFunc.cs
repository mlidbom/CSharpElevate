using System;
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
            Func<int, int> square = parm => parm*parm;
            Action<int> mockSaveFunc = parm => { };
        }

        [Test]
        public void ShouldAlwaysBeUsedBecauseOfCompatibilityIssues()
        {
            IntPredicate largerThan5 = param => param > 5;

            // Compile time error. IntPredicate not compatible with Func<int,bool> that Where takes
            //var sixThroughTen = 1.Through(10).Where(largerThan5);
            var sixThroughTen = 1.Through(10).Where(me => largerThan5(me));
        }
    }
}