using System;

namespace Moon.Validation
{
    /// <summary>
    /// Helper used to retrieve error messages and display names.
    /// </summary>
    public static class ValidationTexts
    {
        /// <summary>
        /// Gets or sets the error message and display name provider.
        /// </summary>
        public static IValidationTextProvider Provider { get; set; }

        /// <summary>
        /// Returns a display name for the given object type.
        /// </summary>
        /// <param name="objectType">The metadata describing validated object or property.</param>
        public static string GetDisplayName(Type objectType)
        {
            EnsureIsConfigured();
            return Provider.GetDisplayName(objectType);
        }

        /// <summary>
        /// Returns a display name for the specified property.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        public static string GetDisplayName(Type objectType, string propertyName)
        {
            EnsureIsConfigured();
            return Provider.GetDisplayName(objectType, propertyName);
        }

        /// <summary>
        /// Returns an error message for the given object type.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public static string GetErrorMessage(Type objectType, string validatorName, string messageKey = null)
        {
            EnsureIsConfigured();
            return Provider.GetErrorMessage(objectType, validatorName, messageKey);
        }

        /// <summary>
        /// Returns an error message for the specified property.
        /// </summary>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public static string GetErrorMessage(Type objectType, string propertyName, string validatorName, string messageKey = null)
        {
            EnsureIsConfigured();
            return Provider.GetErrorMessage(objectType, propertyName, validatorName, messageKey);
        }

        static void EnsureIsConfigured()
        {
            if (Provider == null)
            {
                throw new Exception("The text provider is not configured.");
            }
        }
    }
}