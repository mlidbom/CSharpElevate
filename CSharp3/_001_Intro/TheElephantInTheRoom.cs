using System;
using System.IO;
using System.Linq;
using CSharp3.Extensions.IO;
using NUnit.Framework;

namespace CSharp3._001_Intro
{
    /// <summary>
    /// 
    /// We are breaking the DRY and SRP principles of SOLID with 
    /// most every function we write. 
    /// 
    /// </summary>
    [TestFixture]
    public class TheElephantInTheRoom
    {
        #region setup

        private readonly DirectoryInfo DesktopDirectory = 
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop).AsDirectory();

        #endregion

        #region A bad way

        private static long SizeOfDirectoryClassic(DirectoryInfo directory)
        {
            long result = 0;
            var subDirectories = directory.GetDirectories();
            foreach (var subDirectory in subDirectories)
            {
                result += SizeOfDirectoryClassic(subDirectory);
            }

            var files = directory.GetFiles();
            foreach (var file in files)
            {
                result += file.Length;
            }
            return result;
        }

        private void UseClassic()
        {
            var directorySize = SizeOfDirectoryClassic(DesktopDirectory);
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
        //Multipurpose lines makes reading harder
        //
        //temporary variables makes reading harder

        #endregion

        #endregion

        #region A better way

        [Test]
        private void UseAcceptableVersion()
        {
            var directorySize = DesktopDirectory.Size();
        }

        //Good code reads as a description of it's task.
        //For all files recursively fetched down from the directory, sum their sizes.
        public static long Size(/*this*/ DirectoryInfo directory)
        {
            return directory.FilesResursive()
                .Sum(file => file.Length);
        }               

        #endregion

        #region Tests

        [Test]
        public void BothReturnNonSeroSizesThatAreTheSame()
        {
            var sizeClassic = SizeOfDirectoryClassic(DesktopDirectory);
            var sizeAcceptable = DesktopDirectory.Size();

            Assert.That(sizeClassic, Is.EqualTo(sizeAcceptable));
            Console.WriteLine(sizeAcceptable); //Compare with total commander...            
        }

        #endregion
    }
}