using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp4._020_GenericVariance.Examples.Hierarchies;

namespace CSharp4._020_GenericVariance.Examples.IO
{
    public static class DirectoryInfoEx
    {
        public static DirectoryInfo AsDirectory(this string path)
        {
            return new DirectoryInfo(path);
        }

        public static long Size(this DirectoryInfo directory)
        {
            return directory.FilesResursive()
                .Sum(file => file.Length);
        }

        public static IEnumerable<FileInfo> FilesResursive(this DirectoryInfo directory)
        {
            return directory.DirectoriesRecursive()
                .SelectMany(subdir => subdir.GetFiles());
        }

        public static IEnumerable<DirectoryInfo> DirectoriesRecursive(this DirectoryInfo directory)
        {
            return directory.FlattenHierarchy(me => me.GetDirectories());
        }
    }
}