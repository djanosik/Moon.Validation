using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Provides a model validator for the specified validator type.
    /// </summary>
    /// <typeparam name="TAttribute">The validation attribute.</typeparam>
    public class CompareAttributeAdapter<TAttribute> : DependentAttributeAdapter<TAttribute>
        where TAttribute : CompareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareAttributeAdapter{TAttribute}" /> class.
        /// </summary>
        /// <param name="validator">The validation attribute.</param>
        /// <param name="stringLocalizer">The string localizer.</param>
        public CompareAttributeAdapter(TAttribute validator, IStringLocalizer stringLocalizer)
            : base(validator, stringLocalizer)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "compare";

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("operator", Attribute.Operator.Name);
            return parameters;
        }
    }
}