using System.Collections.Generic;
using NUnit.Framework;

namespace CSharp3.Prerequisites.Enumerators
{
    [TestFixture]
    public class Enumerators
    {
        [Test]
        public void MakeImplementingIEnumerableReallySimple()
        {
            int current = 0;
            foreach (var i in AllInts)
            {
                Assert.That(i, Is.EqualTo(current++));
                if (current > 10)
                {
                    break;
                }
            }
        }


        ///The magic happens because we use the yield keyword. 
        ///It causes the compiler to essentially replace this method with a class 
        ///that implements IEnumerable<int> in such a way as to make each call to 
        /// movenext the equivalent of continuing the execution of this method until 
        /// the next yield statement.
        public IEnumerable<int> AllInts
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
    }
}