using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JamPlace.IdentityServer4.Utils
{
    public static class CustomPathHelper
    {
        public static string GetRootPath()
        {
            var path = Path.GetDirectoryName(System.Reflection
                .Assembly.GetExecutingAssembly().CodeBase);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(path).Value;
                return appRoot;
            }
            return Directory.GetCurrentDirectory();
        }
    }
}
