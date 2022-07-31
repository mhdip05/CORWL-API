using AutoMapper;

namespace NMS_API_N.Helper
{
    public class StringTrimmerProfile : ITypeConverter<string, string>
    {
        public string Convert(string source, string destination, ResolutionContext context)
        {
            var data = source == null ? source : source.Trim();
#nullable disable
            return data;
        }
    }
}
