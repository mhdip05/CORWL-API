using AutoMapper;
using System.Text.RegularExpressions;

namespace CORWL_API.Helper
{
    public class StringTrimmerHelper
    {
#nullable disable
        public static string TrimString(string source)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            return (Regex.Replace(source, @"\s+", " ")).Trim();
        }

    }
}
