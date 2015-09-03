using System;

namespace Moon.Validation
{
    /// <summary>
    /// Defines basic contract for error message and display name providers.
    /// </summary>
    public interface IValidationTextProvider
    {
        /// <summary>
        /// Returns a display name for the given object type.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        string GetDisplayName(Type objectType);

        /// <summary>
        /// Returns a display name for the specified property.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        string GetDisplayName(Type objectType, string propertyName);

        /// <summary>
        /// Returns an error message for the given object type.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        string GetErrorMessage(Type objectType, string validatorName, string messageKey);

        /// <summary>
        /// Returns an error message for the specified property.
        /// </summary>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        string GetErrorMessage(Type objectType, string propertyName, string validatorName, string messageKey);
    }
}