using System.Collections.Generic;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Moon.Validation;

namespace Moon.AspNet.Validation
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
        /// Returns an enumeration of client validation rules.
        /// </summary>
        /// <param name="context">The validation context.</param>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ClientModelValidationContext context)
        {
            Attribute.OtherPropertyDisplayName = GetOtherPropertyDisplayName(context);
            return base.GetClientValidationRules(context);
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
                var otherProperty = context.MetadataProvider.GetMetadataForProperty(
                    metadata.ContainerType,
                    Attribute.OtherProperty);
                if (otherProperty != null)
                {
                    return otherProperty.GetDisplayName();
                }
            }

            return Attribute.OtherProperty;
        }
    }
}