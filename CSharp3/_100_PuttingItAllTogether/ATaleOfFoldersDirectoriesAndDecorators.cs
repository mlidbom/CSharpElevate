using System;
using System.IO;
using CSharp3.Extensions.IO;
using NUnit.Framework;

namespace CSharp3._100_PuttingItAllTogether
{
    [TestFixture]
    public class ATaleOfFoldersDirectoriesAndDecorators
    {
        #region setup

        private static readonly DirectoryInfo DesktopDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop).AsDirectory();

        #endregion

        [Test]
        public void DecomposingFolderSizeInteRolesAndResponsibilities()
        {
            //Defined in the implementation :

            //Every interface and class is immutable.
            //Every method is a pure function.
            //Every metod is an extension method. (Virtual data is exposed as properties, not methods)
            //Every class, interface, and method has a single well defined responsibility.

            //Note the correlation between responsibility and code
            var directorySize = DesktopDirectory.Size();
        }
    }
}