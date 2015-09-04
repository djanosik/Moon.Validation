using System;

namespace Moon.Validation
{
    /// <summary>
    /// Configures the <see cref="Type" /> to use when looking for validation resources.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidationResourcesAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResourcesAttribute" /> class.
        /// </summary>
        /// <param name="resourceType">The resource type to use for string lookup.</param>
        public ValidationResourcesAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }

        /// <summary>
        /// Gets the resource type to use for string lookup.
        /// </summary>
        public Type ResourceType { get; }
    }
}