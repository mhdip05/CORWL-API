using AutoMapper;
using System.Text.RegularExpressions;

namespace CORWL_API.Helper
{
    public class StringTrimmerProfile : ITypeConverter<string, string>
    {
#nullable disable
        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                source = null;
            }

            var data = source == null ? source : (Regex.Replace(source, @"\s+", " ")).Trim();

            return data;
        }

    }
}
