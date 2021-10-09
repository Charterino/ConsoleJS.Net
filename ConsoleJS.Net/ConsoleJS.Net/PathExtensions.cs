using System;
using System.IO;

namespace ConsoleJS.Net
{
    public static class PathExtensions
    {
        public static bool ExistsInPath(this string fileName)
        {
            if (File.Exists(fileName))
                return true;

            var values = Environment.GetEnvironmentVariable("PATH");
            foreach (var path in values.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, fileName);
                if (File.Exists(fullPath))
                    return true;
            }
            return false;
        }

    }
}