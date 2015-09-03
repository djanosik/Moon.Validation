using System;
using System.Collections.Generic;
using System.Linq;

namespace Moon.Validation
{
    /// <summary>
    /// The base class for text providers.
    /// </summary>
    public abstract class ValidationTextProvider : IValidationTextProvider
    {
        /// <summary>
        /// Returns a display name for the given object type.
        /// </summary>
        /// <param name="objectType">The metadata describing validated object or property.</param>
        public abstract string GetDisplayName(Type objectType);

        /// <summary>
        /// Returns a display name for the specified property.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        public abstract string GetDisplayName(Type objectType, string propertyName);

        /// <summary>
        /// Returns an error message for the given object type.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public abstract string GetErrorMessage(Type objectType, string validatorName, string messageKey);

        /// <summary>
        /// Returns an error message for the specified property.
        /// </summary>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public abstract string GetErrorMessage(Type objectType, string propertyName, string validatorName, string messageKey);

        /// <summary>
        /// Enumerates keys that can be used to lookup display name for the given type.
        /// </summary>
        /// <remarks>
        /// <para>Returned keys: <list type="number"><item>T:{LastNamespacePart}_{ObjectTypeName}</item><item>T:{ObjectTypeName}</item></list></para></remarks>
        /// <param name="objectType">The type of the validated object.</param>
        protected IEnumerable<string> GetDisplayNameKeys(Type objectType)
        {
            Requires.NotNull(objectType, nameof(objectType));

            if (objectType.Namespace != null)
            {
                var namespacePart = objectType.Namespace.Split('.').Last();
                yield return $"T:{namespacePart}_{objectType.Name}";
            }

            yield return $"T:{objectType.Name}";
        }

        /// <summary>
        /// Enumerates keys that can be used to lookup display name for the specified property.
        /// </summary>
        /// <remarks>
        /// <para>Returned keys: <list type="number"><item>{LastNamespacePart}_{ObjectTypeName}_{PropertyName}</item><item>{ObjectTypeName}_{PropertyName}</item><item>{PropertyName}</item></list></para></remarks>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        protected IEnumerable<string> GetDisplayNameKeys(Type objectType, string propertyName)
        {
            Requires.NotNull(objectType, nameof(objectType));
            Requires.NotNull(propertyName, nameof(propertyName));

            if (objectType.Namespace != null)
            {
                var namespacePart = objectType.Namespace.Split('.').Last();
                yield return $"{namespacePart}_{objectType.Name}_{propertyName}";
            }

            yield return $"{objectType.Name}_{propertyName}";
            yield return $"{propertyName}";
        }

        /// <summary>
        /// Enumerates keys that can be used to lookup error message for the given type and validator.
        /// </summary>
        /// <remarks>
        /// <para>Returned keys: <list type="number"><item>Error_{ValidatorName}</item></list></para></remarks>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        protected IEnumerable<string> GetErrorMessageKeys(Type objectType, string validatorName)
        {
            Requires.NotNull(objectType, nameof(objectType));

            yield return $"Error_{validatorName}";
        }

        /// <summary>
        /// Enumerates keys that can be used to lookup error message the specified property and validator.
        /// </summary>
        /// <remarks>
        /// <para>Returned keys: <list type="number"><item>{LastNamespacePart}_{ObjectTypeName}_{PropertyName}_{ValidatorName}</item><item>{ObjectTypeName}_{PropertyName}_{ValidatorName}</item><item>{PropertyName}_{ValidatorName}</item><item>Error_{ValidatorName}</item></list></para></remarks>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        protected IEnumerable<string> GetErrorMessageKeys(Type objectType, string propertyName, string validatorName)
        {
            Requires.NotNull(objectType, nameof(objectType));
            Requires.NotNull(propertyName, nameof(propertyName));

            if (objectType.Namespace != null)
            {
                var namespacePart = objectType.Namespace.Split('.').Last();
                yield return $"{namespacePart}_{objectType.Name}_{propertyName}_{validatorName}";
            }

            yield return $"{objectType.Name}_{propertyName}_{validatorName}";
            yield return $"{propertyName}_{validatorName}";
            yield return $"Error_{validatorName}";
        }
    }
}