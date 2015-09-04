using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Mvc.ModelBinding.Metadata;
using Moon.Validation;

namespace Moon.AspNet.Validation
{
    /// <summary>
    /// The convention-based model metadata provider fro ASP.NET MVC.
    /// </summary>
    public class ValidationMetadataProvider : IDisplayMetadataProvider, IValidationMetadataProvider
    {
        readonly ITextProvider textProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationClientValidatorProvider" /> class.
        /// </summary>
        /// <param name="textProvider">The validation text provider.</param>
        public ValidationMetadataProvider(ITextProvider textProvider)
        {
            Requires.NotNull(textProvider, nameof(textProvider));

            this.textProvider = textProvider;
        }

        /// <summary>
        /// Gets the values for properties of <see cref="DisplayMetadata" />.
        /// </summary>
        /// <param name="context">The provider context.</param>
        public void GetDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var displayMetadata = context.DisplayMetadata;

            if (displayMetadata.DisplayName == null || displayMetadata.DisplayName() == null)
            {
                var propertyName = context.Key.Name;
                var objectType = context.Key.ContainerType ?? context.Key.ModelType;

                displayMetadata.DisplayName = () => propertyName != null
                    ? textProvider.GetDisplayName(objectType, propertyName)
                    : textProvider.GetDisplayName(objectType);
            }
        }

        /// <summary>
        /// Gets the values for properties of <see cref="ValidationMetadata" />.
        /// </summary>
        /// <param name="context">The provider context.</param>
        public void GetValidationMetadata(ValidationMetadataProviderContext context)
        {
            var validationMetadata = context.ValidationMetadata;

            foreach (var attribute in validationMetadata.ValidatorMetadata.OfType<ValidationAttribute>())
            {
                LocalizeErrorMessage(context, attribute);
            }
        }

        void LocalizeErrorMessage(ValidationMetadataProviderContext context, ValidationAttribute attribute)
        {
            var propertyName = context.Key.Name;
            var objectType = context.Key.ContainerType ?? context.Key.ModelType;
            var messageKey = attribute.ErrorMessageResourceName;
            var validatorName = attribute.GetValidatorName();

            attribute.ErrorMessageResourceType = null;
            attribute.ErrorMessageResourceName = null;

            attribute.SetOtherDisplayName(p => textProvider.GetDisplayName(objectType, p));

            attribute.ErrorMessage = propertyName != null
                ? textProvider.GetErrorMessage(objectType, propertyName, validatorName, messageKey)
                : textProvider.GetErrorMessage(objectType, validatorName, messageKey);
        }
    }
}