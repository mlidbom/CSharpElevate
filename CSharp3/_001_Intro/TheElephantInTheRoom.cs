using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Void.Hierarchies;
using Void.IO;

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
        /// This is about how I usually see code like this.
        /// Unless the coder is afraid of recursion and makes 
        /// it far uglier still that is!
        /// </summary>
        private static long SizeOfDirectoryClassic(string directory)
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

        // Fetching data, recursive descent tree walking, 
        // transforming data, and data aggregation 
        // is all baked into one big mess.
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

        #endregion

        #region A better way


        private static long SizeOfDirectorySane(string directory)
        {
            return directory.AsHierarchy(Directory.GetDirectories)
                .Flatten()
                .SelectMany(dir => Directory.GetFiles(dir.Wrapped))
                .Sum(file => new FileInfo(file).Length);
        }

        private static long SizeOfDirectoryAcceptable(string path)
        {
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