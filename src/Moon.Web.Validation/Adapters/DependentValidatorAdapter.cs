using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator.</typeparam>
    public abstract class DependentValidatorAdapter<TValidator> : ValidatorAdapter<TValidator>
        where TValidator : DependentValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependentValidatorAdapter{TValidator}" /> class.
        /// </summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="validator">The validator for the model.</param>
        protected DependentValidatorAdapter(ModelMetadata metadata, ControllerContext context, TValidator validator)
            : base(metadata, context, validator)
        {
            if (string.IsNullOrWhiteSpace(Attribute.OtherPropertyDisplayName))
            {
                Attribute.OtherPropertyDisplayName = GetOtherPropertyDisplayName();
            }
        }

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("other", BuildOtherPropertyId());
            return parameters;
        }

        string BuildOtherPropertyId()
        {
            var viewContext = ControllerContext as ViewContext;
            string otherPropertyId = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(Attribute.OtherProperty);

            // This will have the name of the current field appended to the beginning, because the
            // TemplateInfo's context has had this fieldName appended to it. Instead, we want to get
            // the context as though it was one level higher.

            var unwantedPart = string.Format("{0}_", Metadata.PropertyName);

            if (otherPropertyId.StartsWith(unwantedPart, StringComparison.CurrentCulture))
            {
                otherPropertyId = otherPropertyId.Substring(unwantedPart.Length);
            }

            return otherPropertyId;
        }

        string GetOtherPropertyDisplayName()
        {
            if (Metadata.ContainerType != null)
            {
                return ModelMetadataProviders.Current.GetMetadataForProperty(() => Metadata.Model,
                    Metadata.ContainerType, Attribute.OtherProperty).GetDisplayName();
            }

            return Attribute.OtherProperty;
        }
    }
}