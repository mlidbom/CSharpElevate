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
        private readonly DirectoryInfo DesktopDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).AsDirectory();
        #endregion

        [Test]
        public void DecomposingFolderSizeInteRolesAndResponsibilities()
        {
            long directorySize = DesktopDirectory.Size();
        }
    }
}