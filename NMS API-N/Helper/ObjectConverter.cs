using System.Reflection;
namespace NMS_API_N.Helper
{
    public static class ObjectConverter
    {
#nullable disable
        public static void MakeObjectTrim(object obj)
        {
            foreach (var property in GetStringProperties(obj))
            {
                string currentValue = (string)property.GetValue(obj, null);
                if (currentValue != null)
                    property.SetValue(obj, currentValue.Trim(), null);
            }
        }

        public static void ConvertToLower(object obj)
        {
            foreach (var property in GetStringProperties(obj))
            {
                string currentValue = (string)property.GetValue(obj, null);
                if (currentValue != null)
                    property.SetValue(obj, currentValue.ToLower());
            }
        }

        private static IEnumerable<PropertyInfo> GetStringProperties(object obj)
        {
            return obj.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
        }

    }
}
