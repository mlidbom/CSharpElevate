using System;
using NUnit.Framework;

namespace CSharp3._200_Extra_Credit
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
            var twoAndFourAndEightAndSixteen = MakePair(MakePair(2, 4), MakePair(8, 16));
            //What is the type?
            Assert.That(Second(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(16));
            Assert.That(First(Second(twoAndFourAndEightAndSixteen)), Is.EqualTo(8));
            Assert.That(First(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(2));
            Assert.That(Second(First(twoAndFourAndEightAndSixteen)), Is.EqualTo(4));
        }

        //Does it matter what the type of twoAndFour is if it satisfies the 
        //contract specified by the above rules?
        //Maybe not knowing the type can actually be an advantage, 
        //It removes unnesserary detail, the essence of abstracting.

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

        #region Deep woodoo

        private readonly Func<int, int, Func<Func<int, int, int>, int>> MakePair =
            (first, second) => selector => selector(first, second);

        private readonly Func<Func<Func<int, int, int>, int>, int> First =
            pair => pair((first, _) => first);

        private readonly Func<Func<Func<int, int, int>, int>, int> Second =
            pair => pair((_, second) => second);

        //C# is not a bit noisy huh? Would you look at those type definitions!

        //Here's the same in F# with the twist that these pairs are once again
        //fully generic and closed over the set of all objects:
        //
        //  let MakePair =  fun first second -> fun selector -> selector first second
        //  let First  = fun pair -> pair (fun first second -> first)
        //  let Second = fun pair -> pair (fun first second -> second)

        #endregion
    }
}