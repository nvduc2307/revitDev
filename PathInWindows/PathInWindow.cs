using System.Reflection;
using System;
using System.IO;

namespace RevitDev.PathInWindows
{
    internal class PathInWindow
    {
        public static string PathData
        {
            get
            {
                return AssemblyDirectory + "\\Resources\\data";
            }
        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
