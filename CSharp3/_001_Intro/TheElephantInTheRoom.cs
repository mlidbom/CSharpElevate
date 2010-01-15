using System;
using System.IO;
using System.Linq;
using CSharp3.Extensions.Hierarchies;
using CSharp3.Util.Hierarchies;
using CSharp3.Extensions.IO;
using NUnit.Framework;

namespace CSharp3._001_Intro
{
    /// <summary>
    /// There is an elephant in the room. A large problem with the way 
    /// that we write software. 
    /// 
    /// We are breaking the DRY principle of SOLID badly with 
    /// most every function we write. 
    /// 
    /// The elephant is the constant reimplementation 
    /// of generic algorithms. We do it again and again, numb
    /// from years of doing it because we had no no good alternative. NOW WE DO!
    /// 
    /// </summary>
    [TestFixture]
    public class TheElephantInTheRoom
    {
        #region The wrong way

        /// <summary>
        /// This is how I usually see code like this.
        /// Unless the coder is afraid of recursion and makes 
        /// it far uglier still that is!
        /// </summary>
        public static long SizeOfDirectoryClassic(string directory)
        {
            long result = 0;
            var subDirectorys = Directory.GetDirectories(directory);
            foreach (var subDirectory in subDirectorys)
            {
                result += SizeOfDirectoryClassic(subDirectory);
            }

            var files = Directory.GetFiles(directory);
            foreach (var file in files)
            {
                result += new FileInfo(file).Length;
            }

            return result;
        }

        #endregion

        #region And what's wrong with that?

        // String to directory conversion, 
        // Fetching data, recursive descent, 
        // transforming data, and data aggregation 
        // are all baked into one big mess.
        //
        // Code should do one thing at a time in clear and easy to read steps.        
        //        
        // Wheels reinvented above: 
        //  Recursive descent tree walking
        //  Transforming data
        //  Flattening data
        //  Aggregating data
        //
        //In terms of SOLID this translates into violations of;
        //1.SRP since the function is supposed to calculate the size of 
        //a directory but takes upon itself to implement the wheels above.
        //
        //2. DRY since you are reinventing the wheel several times over.
        //
        //3. OCP since changing algorithms will change the function, 
        // not only changes in domain rules for it primary purpose as it should be
        //
        //

        #endregion

        #region A better way


        private static long SizeOfDirectorySane(string directory)
        {
            return directory.AsHierarchy(Directory.GetDirectories)
                .Flatten()
                .SelectMany(dir => Directory.GetFiles(dir.Wrapped))
                .Sum(file => new FileInfo(file).Length);
        }

        #region still complaining!?
        //Yes!
        //The code still works at several levels of abstraction. 
        //The discoverability of an ordinary static method is terrible so dry is likely to be violated.
        #endregion

        #endregion

        #region getting there
        
        private static long SizeOfDirectoryAcceptable(string path)
        {
            //The line below code would likely be inlined in real code. 
            //This method serves little purpose once it's implementation has been refactored to this stage.
            return path.AsDirectory().Size();
        }
        
        #endregion 

        #region Yes it works

        [Test]
        public void BothReturnNonSeroSizesThatAreTheSame()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var sizeClassic = SizeOfDirectoryClassic(directory);
            var sizeSane = SizeOfDirectorySane(directory);
            var sizeBest = SizeOfDirectoryAcceptable(directory);

            Assert.That(sizeClassic, Is.EqualTo(sizeSane));
            Assert.That(sizeBest, Is.EqualTo(sizeSane));
            Console.WriteLine(sizeSane); //Compare with total commander...            
            Console.WriteLine(sizeBest);
        }

        #endregion
    }
}