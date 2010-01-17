using System;
using System.Collections.Generic;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using CSharp3.Util;
using NUnit.Framework;

namespace CSharp3._080_Linq._010_ExtensionMethods
{
    /// <summary>
    ///     
    /// Again and again, in most codefiles you see reinvention of   
    ///     filtering
    ///     transforming
    ///     sorting
    ///     aggregating
    ///     combining
    ///     etc etc
    /// 
    /// Talk about a code smell. Remember DRY?
    /// The functional programming world is laughing at us. 
    /// They solved this before there were PCs!
    /// 
    /// How do they solve it? Functional decomposition
    ///     filter -> Where
    ///     map -> Select
    ///     fold/reduce -> Aggregate(different overloads)
    ///     zip -> Zip Only in 4.0
    ///     
    /// </summary>
    [TestFixture]
    public class FundamentalOperators : NUnitTestBase
    {
        private static IEnumerable<T> Seq<T>(params T[] elements)
        {
            return elements;
        }

        #region filtering

        [Test]
        public void Where()
        {
            var oddsIn1Through10 = 1.Through(4).Where(number => number > 2); //3,4

            oddsIn1Through10 = from number in 1.Through(4)
                               where number > 2
                               select number; //3,4
            #region asserts

            Assert.That(oddsIn1Through10, Is.EqualTo(Seq(3, 4)));
            Assert.That(oddsIn1Through10, Is.EqualTo(Seq(3, 4)));

            #endregion
        }

        [Test]
        public void Distinct()
        {
            var distinct = Seq(1, 1, 2, 2).Distinct(); //1,2
            #region asserts
            Assert.That(distinct, Is.EqualTo(Seq(1, 2)));
            #endregion
        }

        [Test]
        public void OfType()
        {
            var numbers = Seq<object>(1, 2, 3.0, 4.0);
            var integers = numbers.OfType<int>();//1,2
                       
            Assert.That(integers.ToList(), Is.EqualTo(Seq(1,2)));
        }

        #endregion

        #region projections

        [Test]
        public void SelectProjects()
        {
            var doubleOf1Through4 = 1.Through(2).Select(me => me*2);//2,4
            Assert.That(doubleOf1Through4, Is.EqualTo(Seq(2, 4)));

            doubleOf1Through4 = from number in 1.Through(2)
                                select number*2;
            
            Assert.That(doubleOf1Through4, Is.EqualTo(Seq(2, 4)));
        }

        [Test]
        public void SelectManyProjectsAndFlattens()
        {
            var oneThrough4Grouped = Seq(Seq(1, 2), Seq(3, 4));
            var oneThrough4Sequential = oneThrough4Grouped.SelectMany(me => me);

            Assert.That(oneThrough4Sequential, Is.EqualTo(Seq(1, 2, 3, 4)));

            oneThrough4Sequential = from grouping in oneThrough4Grouped
                                    from child in grouping
                                    select child;
            Assert.That(oneThrough4Sequential, Is.EqualTo(Seq(1, 2, 3, 4)));
        }

        #endregion

        #region aggregation

        [Test]
        public void BasicAggregationOperators()
        {
            var oneThrough4 = 1.Through(4);

            var min = oneThrough4.Min(); //1                        
            var max = oneThrough4.Max(); //4            
            var sum = oneThrough4.Sum(); //10           
            var average = oneThrough4.Average(); //2.5


            var minDouble = oneThrough4.Min(num => num*2); //2
            var maxDouble = oneThrough4.Max(num => num*2); //8
            var sumDouble = oneThrough4.Sum(num => num*2); //20
            var averageDouble = oneThrough4.Average(num => num*2); //5

            //No comprehension parallels

            #region asserts

            Assert.That(min, Is.EqualTo(1));
            Assert.That(max, Is.EqualTo(4));
            Assert.That(sum, Is.EqualTo(10));
            Assert.That(average, Is.EqualTo(2.5));

            Assert.That(minDouble, Is.EqualTo(2));
            Assert.That(maxDouble, Is.EqualTo(8));
            Assert.That(sumDouble, Is.EqualTo(20));
            Assert.That(averageDouble, Is.EqualTo(5.0));

            #endregion
        }

        [Test]
        public void AggregateAggregatesAndTransforms()
        {
            var names = Seq("Calle", "Oscar");

            var namesCombined = names.Aggregate((aggregate, file) => aggregate + "," + file); //"Calle,Oscar"

            Assert.That(namesCombined, Is.EqualTo("Calle,Oscar"));

            //No comprehension parallel
        }

        #endregion

        [Test]
        public void ExceptFiltersOutAllElementsInTheArgumentSequence()
        {
            Assert.That(
                1.Through(5).Except(2.Through(4)),
                Is.EqualTo(Seq(1, 5)));
        }

        [Test]
        public void GroupByGroups()
        {
            Func<int, bool> isEven = me => me%2 == 0;
            var groupedByEvenUneven = 1.Through(10).GroupBy(isEven);
            var even = groupedByEvenUneven.Where(grouping => grouping.Key).Single();
            var odd = groupedByEvenUneven.Where(grouping => !grouping.Key).Single();

            Assert.That(even, Is.EqualTo(Seq(2, 4, 6, 8, 10)));
            Assert.That(odd, Is.EqualTo(Seq(1, 3, 5, 7, 9)));
        }
    }
}