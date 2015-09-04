using System.Collections.Generic;
using Moon.Validation;

namespace Moon.AspNet.Validation
{
    /// <summary>
    /// Adapter for <see cref="MaxAttribute" /> attribute.
    /// </summary>
    public class MaxAttributeAdapter : AttributeAdapter<MaxAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public MaxAttributeAdapter(MaxAttribute attribute)
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
            parameters.Add("max", Attribute.MaxValue);
            return parameters;
        }
    }
}