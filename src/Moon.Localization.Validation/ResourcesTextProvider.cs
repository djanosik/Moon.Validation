using System;
using System.Linq;
using Moon.Validation;

namespace Moon.Localization.Validation
{
    /// <summary>
    /// The convention-based provider which uses <see cref="Resources" /> to provide error messages
    /// and display names.
    /// </summary>
    public class ResourcesTextProvider : TextProvider
    {
        /// <summary>
        /// Returns a display name for the given object type.
        /// </summary>
        /// <param name="objectType">The metadata describing validated object or property.</param>
        public override string GetDisplayName(Type objectType)
        {
            var displayName = GetDisplayNameKeys(objectType).Select(Resource)
                .FirstOrDefault(x => x != null);

            return displayName ?? objectType.Name;
        }

        /// <summary>
        /// Returns a display name for the specified property.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        public override string GetDisplayName(Type objectType, string propertyName)
        {
            var displayName = GetDisplayNameKeys(objectType, propertyName).Select(Resource)
                .FirstOrDefault(x => x != null);

            return displayName ?? propertyName;
        }

        /// <summary>
        /// Returns an error message for the given object type.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public override string GetErrorMessage(Type objectType, string validatorName, string messageKey)
        {
            if (messageKey != null)
            {
                return Resource(messageKey);
            }

            return GetErrorMessageKeys(objectType, validatorName).Select(Resource)
                .FirstOrDefault(x => x != null);
        }

        /// <summary>
        /// Returns an error message for the specified property.
        /// </summary>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public override string GetErrorMessage(Type objectType, string propertyName, string validatorName, string messageKey)
        {
            if (messageKey != null)
            {
                return Resource(messageKey);
            }

            return GetErrorMessageKeys(objectType, propertyName, validatorName).Select(Resource)
                .FirstOrDefault(x => x != null);
        }

        static string Resource(string key)
            => Resources.Get("Annotations", key) ?? Resources.Get("Annotations", key.Replace("_", ":"));
    }
}