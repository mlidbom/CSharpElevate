using System;
using System.IO;
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

        #region And what's wrong with that?

        // Code should do one thing at a time in clear and easy to read steps.        
        //        
        // Wheels reinvented and merged above: 
        //  Recursive descent
        //  Transforming data
        //  Flattening hierarchical data
        //  Aggregating data
        //
        //violates: SRP, DRY and OCP
        //
        //domain is implicit

        #endregion

        #endregion

        #region A better way

        private static long SizeOfDirectoryAcceptable(string path)
        {
            //inlined
            return path.AsDirectory().Size();
        }

        #endregion

        #region Yes it works

        [Test]
        public void BothReturnNonSeroSizesThatAreTheSame()
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var sizeClassic = SizeOfDirectoryClassic(directory);
            var sizeAcceptable = SizeOfDirectoryAcceptable(directory);

            Assert.That(sizeClassic, Is.EqualTo(sizeAcceptable));
            Console.WriteLine(sizeAcceptable); //Compare with total commander...            
        }

        #endregion
    }
}