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
        static PropertyInfo routeDataProp;

        /// <summary>
        /// Returns the name of the validator.
        /// </summary>
        /// <param name="validator">The validation attribute.</param>
        public static string GetValidatorName(this ValidationAttribute validator)
        {
            var nameProvider = validator as IValidatorNameProvider;

            if (nameProvider == null)
            {
                var validatorType = validator.GetType();

                if (!validatorType.FullName.EndsWith(".Mvc.RemoteAttribute", StringComparison.Ordinal))
                {
                    return validatorType.Name.Replace("Attribute", string.Empty);
                }

                return GetRemoteValidatorName(validator);
            }

            return nameProvider.GetValidatorName();
        }

        static string GetRemoteValidatorName(ValidationAttribute validator)
        {
            if (routeDataProp == null)
            {
                var type = validator.GetType();
                routeDataProp = type.GetTypeInfo().GetDeclaredProperty("RouteData");
            }

            var routeData = (IDictionary<string, object>)routeDataProp.GetValue(validator);
            return routeData["action"]?.ToString();
        }
    }
}