using AutoMapper;

namespace NMS_API_N.Helper
{
    public class EmptyToNullConverter : ITypeConverter<string, string>
    {
#nullable disable
        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (source == null || source == "") return null;
            return source;
        }


    }
}
