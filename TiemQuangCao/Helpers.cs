using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TiemQuangCao
{
    public class Helpers
    {
        /// <summary>
        /// Convert github tag in string format to Version instance.
        /// Github tag has format of v0.0.1. This is the release tag of the repository.
        /// This function will parse the tag and create an instance of Version with those version number.
        /// 
        /// If input tag is null or empty or not in specified string, return null
        /// </summary>
        /// <param name="tag">a github tag string in format v*.*.*</param>
        /// <returns>instance of Version</returns>
        public static Version GithubTagToVersion(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return null;

            string strRegex = @"^v([0-9]+)\.([0-9]+)\.([0-9]+)$";
            var regexMatch = new Regex(strRegex);
            if (regexMatch.IsMatch(tag))
            {
                int[] versions = tag.Replace("v", "").Split(".").Select(v => int.Parse(v)).ToArray();
                return new Version(versions[0], versions[1], versions[2]);
            }

            return null;
        }

        public static string GetRuntimeIdentifier()
        {
            string os = "";
            if (OperatingSystem.IsWindows()) os = "win";
            if (RuntimeInformation.OSArchitecture == Architecture.X64)
                return os + "-x64";
            else if (RuntimeInformation.OSArchitecture == Architecture.X86)
                return os + "-x86";

            return "unknown";
        }
    }
}
