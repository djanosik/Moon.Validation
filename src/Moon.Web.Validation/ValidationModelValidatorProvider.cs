using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// The convention-based model validator provider for ASP.NET MVC.
    /// </summary>
    public class ValidationModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        readonly ITextProvider textProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationModelValidatorProvider" /> class.
        /// </summary>
        /// <param name="textProvider">The validation text provider.</param>
        public ValidationModelValidatorProvider(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }

        /// <summary>
        /// Gets an enumeration of model validators.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <param name="attributes">An enumeration of attributes.</param>
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            if (AddImplicitRequiredAttributeForValueTypes && metadata.IsRequired && !attributes.Any(a => a is RequiredAttribute))
            {
                attributes = attributes.Concat(new[] { new RequiredAttribute() });
            }

            attributes = LocalizeErrorMessages(metadata, attributes);
            return base.GetValidators(metadata, context, attributes);
        }

        IEnumerable<Attribute> LocalizeErrorMessages(ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            var validationAttributes = attributes.OfType<ValidationAttribute>()
                .Where(x => string.IsNullOrEmpty(x.ErrorMessage));

            foreach (var attribute in validationAttributes)
            {
                var validatorName = attribute.GetValidatorName();
                var messageKey = attribute.ErrorMessageResourceName;
                var objectType = metadata.ContainerType ?? metadata.ModelType;

                attribute.ErrorMessageResourceType = null;
                attribute.ErrorMessageResourceName = null;

                attribute.SetOtherDisplayName(name => textProvider.GetDisplayName(objectType, name));

                attribute.ErrorMessage = metadata.PropertyName != null
                    ? textProvider.GetErrorMessage(objectType, metadata.PropertyName, validatorName, messageKey)
                    : textProvider.GetErrorMessage(objectType, validatorName, messageKey);
            }

            return attributes;
        }
    }
}