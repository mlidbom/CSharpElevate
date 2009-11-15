using System;
using NUnit.Framework;


namespace CSharp3._070_LambdaExpressions
{
    /// <summary>
    /// Closure in math
    /// In math a set Y is said to be closed over an operation X 
    /// if the operation X applied to any member of Y results is a member of Y
    /// 
    /// A pair is something that can be created via MakePair 
    /// examined via First and Second such that:
    /// First(MakePair(first, second)) = first
    /// and
    /// Second(MakePair(first, second)) = second
    /// 
    /// Given the above definition of pair th set of all pairs is closed 
    /// over the operation MakePair
    /// </summary>
    [TestFixture]
    public class HowToBuildPairsOutOfNothingAtAll
    {
        [Test]
        public void CallingFirstOnAPairReturnsTheFirstMemberOfThePair()
        {
            var twoAndFour = MakePair(2, 4); //What is the type?
            Assert.That(First(twoAndFour), Is.EqualTo(2));
        }

        [Test]
        public void CallingSecondOnAPairReturnsTheSecondMemberOfThePair()
        {
            var twoAndFour = MakePair(2, 4); //What is the type?
            Assert.That(Second(twoAndFour), Is.EqualTo(4));            
        }

        [Test]
        public void TheSetOfPairsIsIsClosedOverTheOpertionOfMakePair()
        {
            var twoAndFourAndEightAndSixteen = MakePair(MakePair(2, 4), MakePair(8,16)); //What is the type?
            Assert.That(Second(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(16));
            Assert.That(First(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(8));
            Assert.That(First(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(2));
            Assert.That(Second(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(4));            

        }

        //Does it matter what the type of twoAndFour is if it satisfies the contract specified by the above rules?
        //Maybe not knowing the type can actually be an advantage, It removes unnesserary detail, the essence of abstracting.

        #region Magic!

        enum Index { First, Second }
        static Func<Index, T> MakePair<T>(T first, T second)
        {
            return x => x == Index.First ? first : second;
        }

        static T First<T>(Func<Index, T> pair)
        {
            return pair(Index.First);
        }

        static T Second<T>(Func<Index, T> pair)
        {
            return pair(Index.Second);
        } 

        #endregion
    }
}