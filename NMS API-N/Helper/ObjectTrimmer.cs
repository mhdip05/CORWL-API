namespace NMS_API_N.Helper
{
    public static class ObjectTrimmer
    {
        public static void MakeObjectTrim(object obj)
        {
            var stringProperties = obj.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));

#nullable disable
            foreach (var property in stringProperties)
            {
                string currentValue = (string)property.GetValue(obj, null);
                if (currentValue != null)
                    property.SetValue(obj, currentValue.Trim(), null);
            }
        }
    }
}
