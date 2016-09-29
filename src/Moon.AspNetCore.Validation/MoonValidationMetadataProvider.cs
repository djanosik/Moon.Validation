using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Provides convention-based metadata for Data Annotations and Moon.Validation attributes.
    /// </summary>
    public class MoonValidationMetadataProvider : IDisplayMetadataProvider, IValidationMetadataProvider
    {
        private readonly IOptions<MvcDataAnnotationsLocalizationOptions> options;
        private readonly IStringLocalizerFactory stringLocalizerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoonValidationMetadataProvider" /> class.
        /// </summary>
        /// <param name="options">The Data Annotations localization options.</param>
        /// <param name="stringLocalizerFactory">The string localizer factory.</param>
        public MoonValidationMetadataProvider(IOptions<MvcDataAnnotationsLocalizationOptions> options, IStringLocalizerFactory stringLocalizerFactory)
        {
            this.options = options;
            this.stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// Creates values for properties of <see cref="DisplayMetadata" />.
        /// </summary>
        /// <param name="context">The provider context.</param>
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyName = context.Key.Name;
            var displayAttribute = context.Attributes.OfType<DisplayAttribute>().FirstOrDefault();

            if (propertyName != null)
            {
                var stringLocalizer = GetStringLocalizer(context.Key);

                if (stringLocalizer != null && displayAttribute?.ResourceType == null)
                {
                    context.DisplayMetadata.DisplayName = () => displayAttribute?.GetName() ?? stringLocalizer[propertyName];
                }
            }
        }

        /// <summary>
        /// Creates values for properties of <see cref="ValidationMetadata" />.
        /// </summary>
        /// <param name="context">The provider context.</param>
        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            var validationMetadata = context.ValidationMetadata;
            var stringLocalizer = GetStringLocalizer(context.Key);

            if (stringLocalizer != null)
            {
                foreach (var attribute in validationMetadata.ValidatorMetadata.OfType<ValidationAttribute>())
                {
                    SetOtherDisplayName(stringLocalizer, attribute);
                    UpdateErrorMessage(stringLocalizer, context, attribute);
                }
            }
        }

        private IStringLocalizer GetStringLocalizer(ModelMetadataIdentity modelMetadata)
        {
            IStringLocalizer stringLocalizer = null;
            var providerFactory = options.Value.DataAnnotationLocalizerProvider;

            if (stringLocalizerFactory != null && providerFactory != null)
            {
                stringLocalizer = providerFactory(modelMetadata.ContainerType ?? modelMetadata.ModelType,
                    stringLocalizerFactory);
            }

            return stringLocalizer;
        }

        private void SetOtherDisplayName(IStringLocalizer stringLocalizer, ValidationAttribute attribute)
            => attribute.SetOtherDisplayName(otherName => stringLocalizer[otherName]);

        private void UpdateErrorMessage(IStringLocalizer stringLocalizer, ValidationMetadataProviderContext context, ValidationAttribute attribute)
        {
            if (CanUpdateErrorMessage(attribute))
            {
                var propertyName = context.Key.Name;
                var validatorName = attribute.GetValidatorName();

                attribute.ErrorMessage = propertyName != null
                    ? GetErrorMessageKey(stringLocalizer, propertyName, validatorName)
                    : validatorName;
            }
        }

        private string GetErrorMessageKey(IStringLocalizer stringLocalizer, string propertyName, string validatorName)
        {
            var key = $"{propertyName}_{validatorName}";
            return stringLocalizer[key].ResourceNotFound ? $"@_{validatorName}" : key;
        }

        private bool CanUpdateErrorMessage(ValidationAttribute attribute)
            => string.IsNullOrEmpty(attribute.ErrorMessageResourceName) &&
            attribute.ErrorMessageResourceType == null &&
            options.Value.DataAnnotationLocalizerProvider != null;
    }
}