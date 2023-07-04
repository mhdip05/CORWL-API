namespace CORWL_API.Extension
{
    public static class StringExtensions
    {
#nullable disable
        public static string ToCapitalize(this string input) =>

            input switch
            {
                null => null,
                "" => "",
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };

    }
}
