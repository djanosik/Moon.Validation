using System.Reflection;

namespace Moon.Validation
{
    internal static class PropertyInfoExtensions
    {
        public static bool IsPublic(this PropertyInfo propertyInfo)
            => propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsPublic
                || propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic;
    }
}