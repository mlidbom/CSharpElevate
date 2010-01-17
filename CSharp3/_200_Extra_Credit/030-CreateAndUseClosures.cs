using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp3.Extensions;
using CSharp3.Util;
using CSharp3.Util.Linq;
using NUnit.Framework;
using CSharp3._050_ExtensionMethods;
using CSharp3.Extensions.IO;

namespace CSharp3._200_Extra_Credit
{
    /// <summary>
    /// A closure in math is different from the meaning of closure in programming.
    /// In math a set Y is said to be closed over an operation X 
    /// if the result of the operation X applied to any member of Y is a member of Y
    /// 
    /// Why is the mathematical idea of closure important? Because it 
    /// makes building immensely complex logic a simple case of combining
    /// existing operations and operations on, and members of, the set. 
    /// Linq is so hugely powerful because the
    /// set of all IEnumerable instances is closed over most of the Linq operations
    /// since they take ienumerables and return ienumerables
    /// consider: Transform, Where, Join, SelectMany etc
    /// 
    /// Once you have that property creating powerful abstractions
    /// becomes almost trivial because whatever you create gets the 
    /// ability to be easily combined with what already exists for free.
    /// 
    /// Whenever you design an abstraction, look for the opportunity 
    /// to close it over operations.
    /// </summary>
    /// 

    public static class Closures
    {
        public static IEnumerable<T> SkipOneForEvery<T>(this IEnumerable<T> me, int interval)
        {
            var current = interval;
            foreach (var instance in me)
            {
                if (current-- > 0)
                {
                    yield return instance;
                }
                else
                {
                    current = interval;
                }
            }
        }

        /// <summary>
        /// (1,2,3,4,5,6) -> ((1,2),(3,4),(5,6))
        /// </summary>
        public static IEnumerable<Zipping.Pair<T, T>> ConsecutivePairs<T>(this IEnumerable<T> me)
        {
            // I can easily make use of existing operations of the set
            // Skip is provided my Microsoft, I built Zip and SkipOneForEvery, but they 
            //play together completely seamlessly because they close over the set 
            //of all IEnumerable.
            return me.Zip(me.Skip(1))
                     .SkipOneForEvery(1);
        }

        /// <summary>Executes action with me as the parameter.</summary>
        public static void Do<T>(this T me, Action<T> action)
        {
            action(me);
        }
    }

    [TestFixture]
    public class Closure
    {
        [Test]
        public void YourOperationIsEasylyComposableWithAllTheOtherClosedOperationsOnTheSet()
        {
            1.Through(10).ConsecutivePairs().ForEach(Console.WriteLine);
        }

        [Test]
        public void TransformAndDoAllowsYouToWriteThingsInTheOrderTheyShouldBeExecutedWithoutUsingTemporaryVariables()
        {
            //task: print a comma separated list of the names of the files on your Desktop                        
            var aDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            #region the good old way            

            //Doing it the good old way.
            //No extension methods, no lambdas.
            var files = new DirectoryInfo(aDirectory).GetFiles();
            var fileNames = new string[files.Length];
            for (var i = 0; i < files.Length; i++)
            {
                fileNames[i] = files[i].Name;
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
            
            #endregion

            //Let's try it with Linq only. None of my extensions:       
            var fileNamesLinq = new DirectoryInfo(aDirectory)
                .GetFiles()
                .Select(me => me.Name)
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
            aDirectory.AsDirectory()
                      .GetFiles()
                      .Select(me => me.Name)
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
}