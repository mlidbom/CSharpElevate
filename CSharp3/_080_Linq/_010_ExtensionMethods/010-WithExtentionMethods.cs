using System;
using System.IO;
using System.Linq;
using CSharp3._090_PrinciplesViaSolid._020_UseAndCreateClosures;
using NUnit.Framework;
using Void.Hierarchies;
using Void.Linq;

namespace CSharp3._080_Linq._010_ExtensionMethods
{
    /// <summary>
    ///     
    /// Again and again pretty much in most codefiles you see reinvention of   
    ///     filtering
    ///     transforming
    ///     sorting
    ///     aggregating
    ///     combining
    ///     The list goes on and on.
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
    public class WithExtentionMethods
    {
		string aDirectory = Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
		
        [Test]
        public void WhereFilters()
        {
            1.Through(10).Where(number => number % 2 != 0)
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void SelectTransforms()
        {
            Directory.GetFiles(aDirectory)
                .Select(me => new FileInfo(me))
                .ForEach(Console.WriteLine);
        }

        [Test]
        public void SumSums()
        {
            Directory.GetFiles(aDirectory)
                .Sum(me => new FileInfo(me).Length)
                .Do(Console.WriteLine);
        }

        [Test]
        public void AggregateAggregatesAndTransforms()
        {
            Directory.GetFiles(aDirectory)
                .Aggregate((aggregate, file) => aggregate + "," + file)
                .Do(Console.WriteLine);

            1.Through(10)
                .Aggregate(0, (sum, current) => sum + current)
                .Do(Console.WriteLine); //You should really use Sum in this specific case :)
        }

        [Test]
        public void SelectManyFlattens()
        {
            var ints = new[] {new[] {1, 2}, new[] {3, 4}};
            ints.SelectMany(me => me).ForEach(Console.WriteLine);
        }

        [Test]
        public void DistinctRemovesDuplicates()
        {
            Seq.Create(1, 1, 2, 2, 3, 3, 4, 5).Distinct().ForEach(Console.WriteLine);
        }

        [Test]
        public void ExceptFiltersOutAllElementsInTheArgumentSequence()
        {
            1.Through(5).Except(2.Through(4)).ForEach(Console.WriteLine);
        }

        [Test]
        public void GroupByGroups()
        {
            Func<int, bool> isEven = me => me % 2 == 0;
            1.Through(10).GroupBy(isEven)
                .ForEach(group =>
                         {
                             Console.WriteLine("Even? {0}", group.Key);
                             group.ForEach(Console.WriteLine);
                             Console.WriteLine();
                         }
                );
        }

        [Test]
        public void RememberMe()
        {
            aDirectory.FlattenHierarchy(Directory.GetDirectories)
                .SelectMany(dir => Directory.GetFiles(dir))
                .Sum(file => new FileInfo(file).Length)
                .Do(Console.WriteLine);


            //Verbose version

            //Recursive tree walk. Finds all folders below and including "folder" 
            //by recursively calling Directory.GetDirectories
            //and yielding each one
            var sizeOfFolder = aDirectory.FlattenHierarchy(Directory.GetDirectories)

                //transforms the IEnumerable of folder names into an 
                //IEnumerable of string[]with all the files in each folder
                .Select(currentDirectory => Directory.GetFiles(currentDirectory))

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