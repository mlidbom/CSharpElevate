using System;
using System.Collections.Generic;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using CSharp3.Util;
using NUnit.Framework;

namespace CSharp3._080_Linq._010_ExtensionMethods
{
    [TestFixture]
    public class FundamentalOperators : NUnitTestBase
    {
        private static IEnumerable<T> Seq<T>(params T[] elements)
        {
            return elements;
        }

        #region filtering operators

        [Test]
        public void Where()
        {
            1.Through(4).Where(number => number > 2); //3,4

            var oddsIn1Through4 = from number in 1.Through(4)
                                  where number > 2
                                  select number; //3,4
        }

        [Test]
        public void Distinct()
        {
            Seq(1, 1, 2, 2).Distinct(); //1,2
        }

        [Test]
        public void OfType()
        {
            var numbers = Seq<object>(1, 2, 3.0, 4.0);
            numbers.OfType<int>();//1,2                       
        }

        #region partitioning operators

        [Test]
        public void PartioningOperators()
        {
            var numbers = 1.Through(3);//1,2,3

            numbers.Skip(1); //2,3
            numbers.SkipWhile(num => num < 3);//3
            numbers.Take(1);//1
            numbers.TakeWhile(num => num < 3); //1,2
        }

        #endregion

        #endregion

        #region projection operators

        [Test]
        public void Select()
        {
            var doubleOf1Through4 = 1.Through(2).Select(me => me*2);//2,4

            doubleOf1Through4 = from number in 1.Through(2)
                                select number*2;//2,4           
        }

        [Test]
        public void SelectManyProjectsAndFlattens()
        {
            var oneThrough4Grouped = Seq(Seq(1, 2), Seq(3, 4));//{{1,2},{3,4}}
            oneThrough4Grouped.SelectMany(me => me);//{1,2,3,4}

            var unGrouped = from grouping in oneThrough4Grouped
                            from child in grouping
                            select child;//{1,2,3,4}
        }

        #endregion

        #region sorting operators

        [Test]
        public void OrderByAndThenBy()
        {
            var things = new[]
                             {
                                 new {P1 = 1, P2 = 2},
                                 new {P1 = 2, P2 = 2},
                                 new {P1 = 1, P2 = 1},
                                 new {P1 = 2, P2 = 1},
                             };

            things.OrderBy(thing => thing.P1)
                  .ThenBy(thing => thing.P2);
            //{ P1 = 1, P2 = 1 }
            //{ P1 = 1, P2 = 2 }
            //{ P1 = 2, P2 = 1 }
            //{ P1 = 2, P2 = 2 }

            var sortedThings = from thing in things
                               orderby thing.P1, thing.P2
                               select thing;
        }

        #endregion

        #region aggregation operators

        [Test]
        public void BasicAggregationOperators()
        {
            var oneThrough4 = 1.Through(4);

            oneThrough4.Min(); //1                        
            oneThrough4.Max(); //4            
            oneThrough4.Sum(); //10           
            oneThrough4.Average(); //2.5


            oneThrough4.Min(num => num*2); //2
            oneThrough4.Max(num => num*2); //8
            oneThrough4.Sum(num => num*2); //20
            oneThrough4.Average(num => num*2); //5
        }

        [Test]
        public void AggregateAggregatesAndTransforms()
        {
            var names = Seq("Calle", "Oscar");
            names.Aggregate((aggregate, file) => aggregate + "," + file); //"Calle,Oscar"
        }

        #endregion

        #region quantifiers

        [Test]
        public void Quantifiers()
        {
            var oneThrough3 = 1.Through(3);

            oneThrough3.Contains(2);//true
            
            oneThrough3.Any();//true
            oneThrough3.Any(number => number > 4);//false

            oneThrough3.All(number => number <= 3);//true
        }

        #endregion

        #region set operators

        [Test]
        public void SetOperators()
        {
            var oneAndTwo = 1.Through(2);
            var twoAndThree = 2.Through(3);

            oneAndTwo.Except(twoAndThree);//1
            oneAndTwo.Union(twoAndThree);//1,2,3
            oneAndTwo.Intersect(twoAndThree);//2
        }

        #endregion

        #region element operators

        [Test]
        public void ElementOperators()
        {
            var oneThrough3 = 1.Through(3);
            var one = new[] {1};
            var empty = Enumerable.Empty<object>();

            oneThrough3.First();//1
            oneThrough3.FirstOrDefault();//1;
            empty.FirstOrDefault(); //null

            oneThrough3.Last();//3
            oneThrough3.LastOrDefault();//3;

            oneThrough3.ElementAt(1);//2

            one.Single();//1
            empty.SingleOrDefault();//null

            Assert.Throws<InvalidOperationException>(() => oneThrough3.Single());
            Assert.Throws<InvalidOperationException>(() => oneThrough3.SingleOrDefault());
        }

        #endregion

        #region grouping

        [Test]
        public void GroupByGroups()
        {
            Func<int, bool> isEven = me => me%2 == 0;
            
            var groupedByEvenUneven = 1.Through(4).GroupBy(isEven);//{{1,3},{2,4}}

            var even = groupedByEvenUneven.Where(grouping => grouping.Key).Single();//{2,4}

            even = (from number in 1.Through(4)
                    group number by isEven(number)
                    into grouped 
                    where grouped.Key
                    select grouped).Single();//2,4
        }

        #endregion

        #region joining operators

        readonly IEnumerable<Person> _women = new[] {new Person{Surname = "Svensson", Forename = "Lisa"},
                                                     new Person{Surname = "Karlsson", Forename = "Kerstin"}};

        readonly IEnumerable<Person> _men = new[] {new Person{Surname = "Svensson", Forename = "Karl"},
                                                   new Person{Surname = "Karlsson", Forename = "Sven"}};

        [Test]
        public void Join()
        {
            var relations2 = _women.Join(_men,
                                        wife => wife.Surname, husband => husband.Surname, //joinkey selectors
                                        (wife, husband) => new { Wife = wife, Husband = husband }//projection
                                        );
            //{ Wife = { Forename = Lisa, SurName = Svensson }, Husband = { Forename = Karl, SurName = Svensson } }
            //{ Wife = { Forename = Kerstin, SurName = Karlsson }, Husband = { Forename = Sven, SurName = Karlsson } }

            var relations = from wife in _women
                            join husband in _men on wife.Surname equals husband.Surname
                            select new { Husband = husband, Wife = wife };

            relations2.ForEach(Console.WriteLine);
        }

        [Test]
        public void GroupJoin()
        {
            var people = _women.Concat(_men);
            var surNames = people.Select(person => person.Surname).Distinct();

            var families = from surname in surNames
                           join person in people on surname equals person.Surname
                           into family
                           select family;

            families = surNames.GroupJoin(people, surname => surname, person => person.Surname, (surname, family) => family);

            //{
            //  {
            //      { Forename = Lisa, SurName = Svensson }
            //      { Forename = Karl, SurName = Svensson }
            //  }
            //  {
            //      { Forename = Kerstin, SurName = Karlsson }
            //      { Forename = Sven, SurName = Karlsson }
            //  }
            //}
        }

        #endregion








        #region support
        private class Person
        {
            public string Forename;
            public string Surname;
            public override string ToString()
            {
                return string.Format("{{ Forename = {0}, SurName = {1} }}", Forename, Surname);
            }
        }
        #endregion
    }
}