using System;

namespace Backoffice.Services
{
    public static class StringUtils
    {
        public static string SubstringBetween(this string src, char from, char to, int skipFrames = 0)
        {
            var fromIndex = 0;
            var toIndex = -1;

            for (var i = 0; i <= skipFrames; i++)
            {
                toIndex++;
                fromIndex = src.IndexOf(from, toIndex) + 1;

                if (fromIndex == 0)
                    return null;

                toIndex = src.IndexOf(to, fromIndex);
            }

            if (toIndex == 0)
                toIndex = src.Length;

            return SubstringExt(src, fromIndex, toIndex-1);
        }

        private static string SubstringExt(this string src, int from, int to)
        {
            return src.Substring(from, to - from + 1);
        }
        
        public static string SubstringFromString(this string src, string from, int skipCount = 0)
        {
            var fromIndex = 0;

            for (var i = 0; i <= skipCount; i++)
            {
                fromIndex = src.IndexOf(from, fromIndex, StringComparison.Ordinal) + from.Length;

                if (fromIndex == 0)
                    return null;
            }

            return SubstringExt(src, fromIndex, src.Length - 1);
        }
        
        public static string SubstringFromChar(this string src, char from, int skipCount = 0)
        {
            var fromIndex = 0;

            for (var i = 0; i <= skipCount; i++)
            {
                fromIndex = src.IndexOf(from, fromIndex) + 1;

                if (fromIndex == 0)
                    return null;
            }

            return SubstringExt(src, fromIndex, src.Length - 1);
        }

    }
}