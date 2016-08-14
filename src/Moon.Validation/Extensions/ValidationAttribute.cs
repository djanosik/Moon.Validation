using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Moon.Validation
{
    /// <summary>
    /// <see cref="ValidationAttribute" /> extension methods.
    /// </summary>
    public static class ValidationAttributeExtensions
    {
        private static PropertyInfo routeDataProp;

        /// <summary>
        /// Returns the name of the validator.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public static string GetValidatorName(this ValidationAttribute attribute)
        {
            var nameProvider = attribute as IValidatorNameProvider;

            if (nameProvider == null)
            {
                var validatorType = attribute.GetType();

                if (!validatorType.FullName.EndsWith(".Mvc.RemoteAttribute", StringComparison.Ordinal))
                {
                    return validatorType.Name.Replace("Attribute", string.Empty);
                }

                return GetRemoteValidatorName(attribute);
            }

            return nameProvider.GetValidatorName();
        }

        /// <summary>
        /// Sets the <see cref="DependentAttribute.OtherPropertyDisplayName" /> property if the attribute is of type
        /// <see cref="DependentAttribute" />.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        /// <param name="getDisplayName">The function used to get the display name.</param>
        public static void SetOtherDisplayName(this ValidationAttribute attribute, Func<string, string> getDisplayName)
        {
            var dependent = attribute as DependentAttribute;

            if (dependent != null)
            {
                dependent.OtherPropertyDisplayName = getDisplayName(dependent.OtherProperty);
            }
        }

        private static string GetRemoteValidatorName(ValidationAttribute attribute)
        {
            if (routeDataProp == null)
            {
                var type = attribute.GetType();
                routeDataProp = type.GetTypeInfo().GetDeclaredProperty("RouteData");
            }

            var routeData = (IDictionary<string, object>)routeDataProp.GetValue(attribute);
            return routeData["action"]?.ToString();
        }
    }
}