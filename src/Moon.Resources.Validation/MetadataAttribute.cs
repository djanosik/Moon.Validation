using System;

namespace Moon.Validation
{
    /// <summary>
    /// The model metadata configuration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MetadataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataAttribute" /> class.
        /// </summary>
        /// <param name="resourceType">The resource type to use for string lookup.</param>
        public MetadataAttribute(Type resourceType)
        {
            ResourceType = resourceType;
        }

        /// <summary>
        /// Gets the resource type to use for string lookup.
        /// </summary>
        public Type ResourceType { get; }
    }
}