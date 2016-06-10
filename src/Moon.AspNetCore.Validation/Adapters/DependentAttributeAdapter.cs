using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TAttribute">The validation attribute.</typeparam>
    public abstract class DependentAttributeAdapter<TAttribute> : AttributeAdapter<TAttribute>
        where TAttribute : DependentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependentAttributeAdapter{TAttribute}" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        protected DependentAttributeAdapter(TAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Adds client validation attributes.
        /// </summary>
        /// <param name="context">The validation context.</param>
        public override void AddValidation(ClientModelValidationContext context)
        {
            Attribute.OtherPropertyDisplayName = GetOtherPropertyDisplayName(context);
            base.AddValidation(context);
        }

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("other", "*." + Attribute.OtherProperty);
            return parameters;
        }

        string GetOtherPropertyDisplayName(ClientModelValidationContext context)
        {
            var metadata = context.ModelMetadata;
            var otherPropertyDisplayName = Attribute.OtherPropertyDisplayName;

            if (otherPropertyDisplayName == null && metadata.ContainerType != null)
            {
                var otherProperty = context.MetadataProvider.GetMetadataForProperties(metadata.ContainerType)
                    .SingleOrDefault(m => m.PropertyName == Attribute.OtherProperty);

                if (otherProperty != null)
                {
                    return otherProperty.GetDisplayName();
                }
            }

            return Attribute.OtherProperty;
        }
    }
}