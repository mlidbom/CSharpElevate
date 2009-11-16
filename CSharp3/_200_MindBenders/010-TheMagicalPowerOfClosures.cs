using System;
using NUnit.Framework;

namespace CSharp3._200_MindBenders
{
    /// <summary>
    /// 
    /// A pair is something that can be created via MakePair 
    /// and examined via First and Second such that:
    /// First(MakePair(first, second)) = first
    /// and
    /// Second(MakePair(first, second)) = second
    /// 
    /// Given the above definition of pair the set of all pairs is closed 
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
            var twoAndFourAndEightAndSixteen = MakePair(MakePair(2, 4), MakePair(8, 16)); //What is the type?
            Assert.That(Second(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(16));
            Assert.That(First(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(8));
            Assert.That(First(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(2));
            Assert.That(Second(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(4));
        }

        //Does it matter what the type of twoAndFour is if it satisfies the contract specified by the above rules?
        //Maybe not knowing the type can actually be an advantage, It removes unnesserary detail, the essence of abstracting.

        #region Magic!

        private enum Index
        {
            First,
            Second
        }

        private static Func<Index, T> MakePair<T>(T first, T second)
        {
            return x => x == Index.First ? first : second;
        }


        private static T First<T>(Func<Index, T> pair)
        {
            return pair(Index.First);
        }

        private static T Second<T>(Func<Index, T> pair)
        {
            return pair(Index.Second);
        }

        #endregion
    }

    [TestFixture]
    public class GoingDeeper
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
            var twoAndFourAndEightAndSixteen = MakePair(MakePair(2, 4), MakePair(8, 16)); //What is the type?
            Assert.That(Second(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(16));
            Assert.That(First(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(8));
            Assert.That(First(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(2));
            Assert.That(Second(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(4));
        }

        #region Deep woodoo
        //A hack created by Alonzo Church who lived before there was even the simplest computer...

        static Func<Func<T, T, T>, T> MakePair<T>(T first, T second)
        {
            return (selector) => selector(first, second);
        }

        static T First<T>(Func<Func<T, T, T>, T> pair)
        {
            return pair((first, _) => first);
        }

        static T Second<T>(Func<Func<T, T, T>, T> pair)
        {
            return pair((_, second) => second);
        }

        #endregion
    }
}