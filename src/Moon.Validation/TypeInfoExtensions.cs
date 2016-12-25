using System;
using System.Linq;
using System.Reflection;

namespace Moon.Validation
{
    internal static class TypeInfoExtensions
    {
        private static readonly Type[] simpleTypes = {
            typeof(string), typeof(bool), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid),
            typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(float), typeof(double), typeof(decimal)
        };

        public static bool IsSimple(this TypeInfo typeInfo)
            => typeInfo.IsEnum || simpleTypes.Contains(typeInfo.AsType());
    }
}