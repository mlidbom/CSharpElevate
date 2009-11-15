using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Void.Linq;
using System.Linq;
using Void;

namespace CSharp3._090_PrinciplesViaSolid._020_UseAndCreateClosures
{
    /// <summary>
    /// A closure in math is different from the meaning of closure in programming.
    /// In math a set Y is said to be closed over an operation X 
    /// if the operation X applied to any member of Y results is a member of Y
    /// 
    /// Why is the mathematical idea of closure important? Because it 
    /// makes building immensely complex logic a simple case of combining
    /// existing operations and members of the set. Linq is so hugely powerful because the
    /// set of all IEnumerable instances is closed over most of the Linq operations
    /// since they take ienumerables and return ienumerables
    /// consider: Transform, Where, Join, SelectMany etc
    /// 
    /// Once you have that property creating powerful abstractions
    /// becomes almost trivial because whatever you create gets the 
    /// ability to be easily combined with what already exists for free.
    /// 
    /// 
    /// Whenever you design an abstraction, look for the opportunity 
    /// to close it over operations.
    /// </summary>
    [TestFixture]
    public class Closure
    {
        [Test]
        public void YourOperationIsEasylyComposableWithAllTheOtherClosedOperationsOnTheSet()
        {
            //ConsecutivePairs becomes an int
            Enumerable.Range(1,10).ConsecutivePairs().ForEach(Console.WriteLine);
        }

        [Test]
        public void TransformAndDoAllowsYouToWriteThingsInTheOrderTheyShouldBeExecutedWithoutUsingTemporaryVariables()
        {
            //task: print a comma separated list of the names of the files in c:\                        

            //Doing it the good old way.
            //No extension methods, no lambdas.
            string directory = @"C:\";
            var filePaths = Directory.GetFiles(directory);
            var fileNames = new string[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++ )
            {
                fileNames[i] = Path.GetFileName(filePaths[i]);
            }
            var commaSeparatedFiles = string.Join(",", fileNames.ToArray());
            Console.WriteLine(commaSeparatedFiles);

            #region comments

            //That's 9 lines of code that is mostly noise. 

            //Digging out the intended behavior takes notable effort.

            //The code constantly refers to variables that were
            //created several steps ago making it hard to keep track of 
            //what the variables contain. This causes you ta have to 
            //jump back and forth in the code as you read it just to 
            //look up what value a variable contains

            #endregion

            //Let's try it with Linq only. None of my extensions:
            directory = @"C:\";            
            var fileNamesLinq = Directory.GetFiles(directory)
                .Select(me => Path.GetFileName(me))
                .Aggregate((result, current) => result + "," + current);
            Console.WriteLine(fileNamesLinq);

            #region comments

            //That's a lot better. Most operations operate
            // on the result of the previous operation making
            //for a simple linear reading of the code. 

            //But what is this aggregate thing?
            //Why use aggregate when string has Join? 
            //Because you cannot stitch a join operation onto
            //a series of linq operators because linq operators 
            //operate on each instance of a sequence, not the sequence 

            //We also still need one temporary variable. 
            //We should be able to do better.

            #endregion

            //Now let's try it using lambdas and my extension methods 
            //Transform and Do as well as the Linq extension methods Select and ToArray
            @"C:\".Transform(me => Directory.GetFiles(me))
                  .Select(me => Path.GetFileName(me))
                  .Transform(me => string.Join(",", me.ToArray()))
                  .Do(Console.WriteLine);

            #region comments

            //Now that is what I call code that clearly expresses intention.
            //The result of each operation feeds into the 
            //next eliminating the need for temporary 
            //variables and jumping back and forth in the code
            //to figure out where a value came from
            //
            //You can just read it and see which operation each 
            //row performs upon the result of the previous operation.

            #endregion
        }
    }

    public static class LinqExtensionsClosedOverTheSetOfIEnumerable
    {
        public static IEnumerable<T> SkipOneForEvery<T>(this IEnumerable<T> me, int interval)
        {
            int current = interval;
            foreach (var instance in me)
            {
                if(current-- > 0)
                {
                    yield return instance;
                }else
                {
                    current = interval;
                }
            }            
        } 
        /// <summary>
        /// A closed operation of the set of IEnumerables that
        /// return a new IEnumerable that is an IEnumerable of the 
        /// objects in the first IEnuerable paired consecutively
        /// (1,2,3,4,5,6) -> ((1,2),(3,4),(5,6))
        /// </summary>
        public static IEnumerable<Zipping.Pair<T, T>> ConsecutivePairs<T>(this IEnumerable<T> me)
        {
            // I can easily make use of existing operations of the set
            // Skip is provided my Microsoft, I built Zip and SkipOneForEvery, but they 
            //play together completely seamlessly because they close over the set 
            //of all IEnumerable.
            return me.Zip(me.Skip(1)).SkipOneForEvery(1);
        }

        /// <summary>
        /// Applies a transformation to any object. 
        /// It is closed over the most generic possible set in C#
        /// the set of all instances of the class object on subtypes.
        /// </summary>
        public static TReturn Transform<TSource, TReturn>(this TSource me, Func<TSource, TReturn> transform)
        {
            return transform(me);
        }

        public static void Do<T>(this T me, Action<T> action)
        {
            action(me);
        }
    }
}