using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp3._001_Prerequisites._010_Enumerators
{
    [TestFixture]
    public class Enumerators
    {
        ///The magic happens because we use the yield keyword. 
        ///It causes the compiler to essentially replace this method with a class 
        ///that implements IEnumerable in such a way as to make each call to 
        /// movenext the equivalent of continuing the execution of this method until 
        /// the next yield statement.
        private static IEnumerable<int> AllInts
        {
            get
            {
                var current = 0;
                while (current < int.MaxValue)
                {
                    yield return current++;
                }
            }
        }

        [Test]
        public void MakeImplementingIEnumerableReallySimple()
        {
            var current = 0;
            foreach (var i in AllInts)
            {
                Assert.That(i, Is.EqualTo(current++));
                if (current > 10)
                {
                    break;
                }
            }
        }
    }
}