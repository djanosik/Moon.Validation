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
    public class MoonModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
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

            attributes = ModifyValidationAttributes(metadata, attributes);
            return base.GetValidators(metadata, context, attributes);
        }

        IEnumerable<Attribute> ModifyValidationAttributes(ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            var validationAttributes = attributes.OfType<ValidationAttribute>()
                .Where(x => string.IsNullOrWhiteSpace(x.ErrorMessage));

            foreach (var attribute in validationAttributes)
            {
                var validatorName = attribute.GetValidatorName();
                var messageKey = attribute.ErrorMessageResourceName;
                var objectType = metadata.ContainerType ?? metadata.ModelType;

                attribute.ErrorMessageResourceType = null;
                attribute.ErrorMessageResourceName = null;

                attribute.ErrorMessage = !string.IsNullOrWhiteSpace(metadata.PropertyName)
                    ? ValidationTexts.GetErrorMessage(objectType, metadata.PropertyName, validatorName, messageKey)
                    : ValidationTexts.GetErrorMessage(objectType, validatorName, messageKey);
            }

            return attributes;
        }
    }
}