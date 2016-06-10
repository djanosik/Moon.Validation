using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Adapter for <see cref="RequiredIfEmptyAttribute" /> attribute.
    /// </summary>
    public class RequiredIfEmptyAttributeAdapter : DependentAttributeAdapter<RequiredIfEmptyAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfEmptyAttributeAdapter" /> class.
        /// </summary>
        /// <param name="attribute">The validation attribute.</param>
        public RequiredIfEmptyAttributeAdapter(RequiredIfEmptyAttribute attribute)
            : base(attribute)
        {
        }

        /// <summary>
        /// Gets the type of client validation.
        /// </summary>
        protected override string ValidationType
            => "requiredifempty";
    }
}