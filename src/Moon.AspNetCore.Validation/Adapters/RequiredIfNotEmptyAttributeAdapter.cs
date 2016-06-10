using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfNotEmptyAttribute" /> attribute.
    /// </summary>
    public class RequiredIfNotEmptyAttributeAdapter : DependentAttributeAdapter<RequiredIfNotEmptyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfNotEmptyAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public RequiredIfNotEmptyAttributeAdapter(RequiredIfNotEmptyAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "requiredifnotempty";
    }
}