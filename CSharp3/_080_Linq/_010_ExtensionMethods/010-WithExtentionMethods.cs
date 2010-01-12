using System;
using System.IO;
using System.Linq;
using CSharp3._090_PrinciplesViaSolid._020_UseAndCreateClosures;
using CSharp3.Util;
using NUnit.Framework;
using Void.Hierarchies;
using Void.Linq;

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
    public class WithExtentionMethods : NUnitTestBase
    {
        private static readonly string ADirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        [Test]
        public void WhereFilters()
        {
            var oddsIn1Through10 = 1.Through(10).Where(number => number%2 != 0);
            Assert.That(oddsIn1Through10, Is.EqualTo(Seq.Create(1,3,5,7,9)));
        }

        [Test]
        public void SelectTransforms()
        {
            var doubleOf1Through4 = 1.Through(4).Select(me => me * 2);
            Assert.That(doubleOf1Through4, Is.EqualTo(Seq.Create(2,4,6,8)));
        }

        [Test]
        public void SumSums()
        {
            var sumOf1Through4 = 1.Through(4).Sum();
            var sumOf1Through4Explicit = 1.Through(4).Sum(me => me);

            Assert.That(sumOf1Through4, Is.EqualTo(10));
            Assert.That(sumOf1Through4Explicit, Is.EqualTo(10));
        }

        [Test]
        public void AggregateAggregatesAndTransforms()
        {
            var oneThrough4AsStrings = 1.Through(4)
                .Select(me => me.ToString())
                .Aggregate((aggregate, file) => aggregate + "," + file);

            Assert.That(oneThrough4AsStrings, Is.EqualTo("1,2,3,4"));


            var sumOf1Through4  = 1.Through(4)
                .Aggregate(0, (sum, current) => sum + current);

            Assert.That(sumOf1Through4, Is.EqualTo(10));
        }

        [Test]
        public void SelectManyFlattens()
        {
            var oneThrough4Grouped = Seq.Create(Seq.Create(1, 2), Seq.Create(3, 4));
            var oneThrough4Sequential = oneThrough4Grouped.SelectMany(me => me);
            Assert.That(oneThrough4Sequential, Is.EqualTo(Seq.Create(1,2,3,4)));
        }

        [Test]
        public void DistinctRemovesDuplicates()
        {
            var oneThrough5WithDuplicates = Seq.Create(1, 1, 2, 2, 3, 3, 4, 5);
            var oneThrough5Unique = oneThrough5WithDuplicates.Distinct();
            Assert.That(oneThrough5Unique, Is.EqualTo(Seq.Create(1,2,3,4,5)));
        }

        [Test]
        public void ExceptFiltersOutAllElementsInTheArgumentSequence()
        {
            var oneThrough5Except2Through4 = 1.Through(5).Except(2.Through(4));
            Assert.That(oneThrough5Except2Through4, Is.EqualTo(Seq.Create(1,5)));
        }

        [Test]
        public void GroupByGroups()
        {
            Func<int, bool> isEven = me => me % 2 == 0;
            var groupedByEvenUneven = 1.Through(10).GroupBy(isEven);
            var even = groupedByEvenUneven.Where(grouping => grouping.Key).Single();
            var odd = groupedByEvenUneven.Where(grouping => !grouping.Key).Single();
            
            Assert.That(even, Is.EqualTo(Seq.Create(2, 4, 6, 8, 10)));
            Assert.That(odd, Is.EqualTo(Seq.Create(1, 3, 5, 7, 9)));
        }

        [Test]
        public void RememberMe()
        {
            ADirectory.AsHierarchy(Directory.GetDirectories).Flatten()
                .SelectMany(dir => Directory.GetFiles(dir.Wrapped))
                .Sum(file => new FileInfo(file).Length)
                .Do(Console.WriteLine);


            //Verbose version

            //Recursive tree walk. Finds all folders below and including "folder" 
            //by recursively calling Directory.GetDirectories
            //and yielding each one
            var sizeOfFolder = ADirectory.AsHierarchy(Directory.GetDirectories).Flatten()

                //transforms the IEnumerable of folder names into an 
                //IEnumerable of string[]with all the files in each folder
                .Select(currentDirectory => Directory.GetFiles(currentDirectory.Wrapped))

                //flattens the IEnumerable of string[] into an IEnumerable of string
                .SelectMany(files => files)

                //Transforms the filepaths into FileInfo objects
                .Select(file => new FileInfo(file)) //transformation 

                //Transforms the FileInfo objects into longs containing
                //their size
                .Select(fileinfo => fileinfo.Length) //transformation

                //Sums all the file sizes.
                .Sum(); //aggregation.
            Console.WriteLine(sizeOfFolder);
        }
    }
}