using System.IO;
using System.Linq;
using CSharp3.Extensions.Hierarchies;

namespace CSharp3.Extensions.IO
{
    public static class DirectoryInfoEx
    {
        public static DirectoryInfo AsDirectory(this string path)
        {
            return new DirectoryInfo(path);
        }

        public static long Size(this DirectoryInfo directory)
        {
            return directory.AsHierarchy(me => me.GetDirectories())
                .Flatten()
                .Unwrap()
                .SelectMany(subdir => subdir.GetFiles())
                .Sum(file => file.Length);
        }
    }
}