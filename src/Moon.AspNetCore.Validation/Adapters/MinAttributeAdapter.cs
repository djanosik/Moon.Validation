using System.Collections.Generic;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Adapter for <see cref="MinAttribute" /> attribute.
    /// </summary>
    public class MinAttributeAdapter : AttributeAdapter<MinAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public MinAttributeAdapter(MinAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "range";

        /// <summary>
        /// Returns a dictionary containing client validation parameters.
        /// </summary>
        protected override IDictionary<string, object> GetClientValidationParameters()
        {
            var parameters = base.GetClientValidationParameters();
            parameters.Add("min", Attribute.MinValue);
            return parameters;
        }
    }
}