using System;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Moon.Validation
{
    /// <summary>
    /// The convention-based provider which uses standard resources to provide error messages and
    /// display names.
    /// </summary>
    public class ResourcesTextProvider : ValidationTextProvider
    {
        private readonly Type defaultResourceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesTextProvider" /> class.
        /// </summary>
        public ResourcesTextProvider()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesTextProvider" /> class.
        /// </summary>
        /// <param name="defaultResourceType">The default resource type to use for text lookup.</param>
        public ResourcesTextProvider(Type defaultResourceType)
        {
            this.defaultResourceType = defaultResourceType;
        }

        /// <summary>
        /// Returns a display name for the given object type.
        /// </summary>
        /// <param name="objectType">The metadata describing validated object or property.</param>
        public override string GetDisplayName(Type objectType)
        {
            var resourceType = GetResourceType(objectType);

            if (resourceType == null)
            {
                return objectType.Name;
            }

            var resourceKey = GetDisplayNameKeys(objectType)
                .FirstOrDefault(k => HasProperty(resourceType, k));

            if (resourceKey == null)
            {
                return objectType.Name;
            }

            var resourceManager = new ResourceManager(resourceType);
            return resourceManager.GetString(resourceKey);
        }

        /// <summary>
        /// Returns a display name for the specified property.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        public override string GetDisplayName(Type objectType, string propertyName)
        {
            var resourceType = GetResourceType(objectType);

            if (resourceType == null)
            {
                return propertyName;
            }

            var resourceKey = GetDisplayNameKeys(objectType, propertyName)
                .FirstOrDefault(k => HasProperty(resourceType, k));

            if (resourceKey == null)
            {
                return propertyName;
            }

            var resourceManager = new ResourceManager(resourceType);
            return resourceManager.GetString(resourceKey);
        }

        /// <summary>
        /// Returns an error message for the given object type.
        /// </summary>
        /// <param name="objectType">The type of the validated object.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public override string GetErrorMessage(Type objectType, string validatorName, string messageKey)
        {
            var resourceType = GetResourceType(objectType);

            if (resourceType == null)
            {
                return null;
            }

            var resourceKey = messageKey ?? GetErrorMessageKeys(objectType, validatorName)
                .FirstOrDefault(k => HasProperty(resourceType, k));

            if (resourceKey == null)
            {
                return null;
            }

            var resourceManager = new ResourceManager(resourceType);
            return resourceManager.GetString(resourceKey);
        }

        /// <summary>
        /// Returns an error message for the specified property.
        /// </summary>
        /// <param name="objectType">The type the property is defined on.</param>
        /// <param name="propertyName">The name of the validated property.</param>
        /// <param name="validatorName">The name of validator to return message for.</param>
        /// <param name="messageKey">The preferred or default message key.</param>
        public override string GetErrorMessage(Type objectType, string propertyName, string validatorName, string messageKey)
        {
            var resourceType = GetResourceType(objectType);

            if (resourceType == null)
            {
                return null;
            }

            var resourceKey = messageKey ?? GetErrorMessageKeys(objectType, propertyName, validatorName)
                .FirstOrDefault(k => HasProperty(resourceType, k));
            
            if (resourceKey == null)
            {
                return null;
            }

            var resourceManager = new ResourceManager(resourceType);
            return resourceManager.GetString(resourceKey);
        }

        private MetadataAttribute GetMetadataAttribute(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            var attribute = typeInfo.GetCustomAttributes<MetadataAttribute>(true).FirstOrDefault();
            attribute = attribute ?? typeInfo.Assembly.GetCustomAttributes<MetadataAttribute>().FirstOrDefault();
            return attribute;
        }

        private Type GetResourceType(Type type)
        {
            var resourceType = defaultResourceType;

            var metadataAttribute = GetMetadataAttribute(type);
            if (metadataAttribute != null && metadataAttribute.ResourceType != null)
            {
                resourceType = metadataAttribute.ResourceType;
            }

            return resourceType;
        }

        private bool HasProperty(Type type, string propertyName)
        {
            if (type == null || propertyName == null)
            {
                return false;
            }
            var info = type.GetTypeInfo();
            return info.GetDeclaredProperty(propertyName) != null;
        }
    }
}