using System;
using System.Collections.Generic;
using System.Linq;
using CSharp3._050_ExtensionMethods;
using NUnit.Framework;

namespace CSharp3._080_Linq
{
    [TestFixture]
    public class Operators
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

            var greaterThan2 = from number in 1.Through(4)
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
            numbers.OfType<int>(); //1,2                       
            //numbers.Cast<int>().ToArray();
        }

        #region partitioning operators

        [Test]
        public void PartioningOperators()
        {
            var numbers = 1.Through(3); //1,2,3

            numbers.Skip(1); //2,3
            numbers.SkipWhile(num => num < 3); //3
            numbers.Take(2); //1,2
            numbers.TakeWhile(num => num < 3); //1,2
        }

        #endregion

        #endregion

        #region conversion

        [Test]
        public void Cast()
        {
            var objects = Seq<object>(1, 2, 3);
            var numbers = objects.Cast<int>();
        }

        #endregion

        #region projection operators

        [Test]
        public void Select()
        {
            var doubleOf1Through4 = 1.Through(2).Select(me => me*2); //2,4

            doubleOf1Through4 = from number in 1.Through(2)
                                select number*2; //2,4           
        }

        [Test]
        public void SelectManyProjectsAndFlattens()
        {
            var oneThrough4Grouped = Seq(Seq(1, 2), Seq(3, 4)); //{{1,2},{3,4}}
            oneThrough4Grouped.SelectMany(me => me); //{1,2,3,4}

            var unGrouped = from grouping in oneThrough4Grouped
                            from child in grouping
                            select child; //{1,2,3,4}
        }

        #endregion

        #region ordering operators

        [Test]
        public void Ordering()
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
                               orderby thing.P1 , thing.P2
                               select thing;

            sortedThings.Reverse(); //Can you guess?
        }

        #endregion

        #region aggregation operators

        [Test]
        public void BasicAggregationOperators()
        {
            var oneThrough4 = 1.Through(4);

            oneThrough4.Count();//4:int
            oneThrough4.LongCount();//4:long

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
            var names = Seq("Calle", "Oscar", "Sverre");
            names.Aggregate((aggregate, file) => aggregate + "," + file); //"Calle,Oscar,Sverre"
        }

        #endregion

        #region quantifiers

        [Test]
        public void Quantifiers()
        {
            var oneThrough3 = 1.Through(3);

            oneThrough3.Contains(2); //true

            oneThrough3.Any(); //true
            oneThrough3.Any(number => number > 4); //false

            oneThrough3.All(number => number <= 3); //true
        }

        #endregion

        #region set operators

        [Test]
        public void SetOperators()
        {
            var oneAndTwo = 1.Through(2);
            var twoAndThree = 2.Through(3);

            oneAndTwo.Except(twoAndThree); //1
            oneAndTwo.Union(twoAndThree); //1,2,3
            oneAndTwo.Intersect(twoAndThree); //2
            oneAndTwo.Concat(twoAndThree);//1,2,2,3
        }

        #endregion

        #region element operators

        [Test]
        public void ElementOperators()
        {
            var oneThrough3 = 1.Through(3);
            var one = Seq(1);
            var empty = Enumerable.Empty<object>();

            oneThrough3.First(); //1
            oneThrough3.FirstOrDefault(); //1;
            empty.FirstOrDefault(); //null

            oneThrough3.Last(); //3
            oneThrough3.LastOrDefault(); //3;

            oneThrough3.ElementAt(1); //2

            one.Single(); //1
            empty.SingleOrDefault(); //null

            Assert.Throws<InvalidOperationException>(() => oneThrough3.Single());
            Assert.Throws<InvalidOperationException>(() => oneThrough3.SingleOrDefault());
        }

        #endregion

        #region grouping

        [Test]
        public void GroupBy()
        {
            Func<int, bool> isEven = me => me%2 == 0;

            var even = 1.Through(4)
                .GroupBy(isEven)
                .Where(grouping => grouping.Key)
                .Single(); //{2,4}

            even = (from number in 1.Through(4)
                    group number by isEven(number)
                    into grouped
                    where grouped.Key
                    select grouped).Single(); //2,4
        }

        #endregion

        #region joining operators

        private static IEnumerable<Person> Women
        {
            get
            {
                return Seq(new Person {Surname = "Svensson", Forename = "Lisa"},
                           new Person {Surname = "Karlsson", Forename = "Kerstin"});
            }
        }

        private static IEnumerable<Person> Men
        {
            get
            {
                return Seq(new Person {Surname = "Svensson", Forename = "Karl"},
                           new Person {Surname = "Karlsson", Forename = "Sven"});
            }
        }

        private static IEnumerable<Person> People
        {
            get { return Women.Concat(Men); }
        }

        private static IEnumerable<string> SurNames
        {
            get { return People.Select(person => person.Surname).Distinct(); }
        }

        [Test]
        public void Join()
        {
            var spouses = from wife in Women
                          join husband in Men on wife.Surname equals husband.Surname
                          select new Spouses {Wife = wife, Husband = husband};

//{ Husband = { Forename = "Karl", SurName = "Svensson" }, Wife = { Forename = "Lisa", SurName = "Svensson" } }
//{ Husband = { Forename = "Sven", SurName = "Karlsson" }, Wife = { Forename = "Kerstin", SurName = "Karlsson" } }
            spouses = Women.Join(Men,
                                 wife => wife.Surname,
                                 husband => husband.Surname,//joinkey selectors
                                 (wife, husband) => new Spouses {Wife = wife, Husband = husband}
                //projection
                );

            spouses.ForEach(Console.WriteLine);
        }

        [Test]
        public void GroupJoin()
        {
            var families = from surname in SurNames
                           join person in People on surname equals person.Surname
                           into family
                           select family;

            families = SurNames.GroupJoin(People,
                                          surname => surname,
                                          person => person.Surname,//joinkey selectors
                                          (surname, family) => family); //projection

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
                return string.Format("{{ Forename = \"{0}\", SurName = \"{1}\" }}",
                                     Forename,
                                     Surname);
            }
        }

        private class Spouses
        {
            public Person Wife;
            public Person Husband;

            public override string ToString()
            {
                return string.Format("{{ Husband = {0}, Wife = {1} }}", Husband, Wife);
            }
        }

        #endregion
    }
}