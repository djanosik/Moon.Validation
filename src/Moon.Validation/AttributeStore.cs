using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Localization;
using Moon.Collections;

namespace Moon.Validation
{
    /// <summary>
    /// The cache of Validation and Display attributes.
    /// </summary>
    internal class AttributeStore
    {
        private readonly ConcurrentDictionary<Type, TypeItem> items = new ConcurrentDictionary<Type, TypeItem>();
        private readonly IStringLocalizerFactory stringLocalizerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeStore" /> class.
        /// </summary>
        /// <param name="stringLocalizerFactory">The string localizer factory.</param>
        public AttributeStore(IStringLocalizerFactory stringLocalizerFactory)
        {
            this.stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// Retrieves the display name associated with the given type.
        /// </summary>
        /// <param name="objectContext">The context that describes the type.</param>
        public string GetTypeDisplayName(ValidationContext objectContext)
            => GetTypeItem(objectContext.ObjectType).DisplayName;

        /// <summary>
        /// Retrieves the type level validation attributes for the given type.
        /// </summary>
        /// <param name="objectContext">The context that describes the type.</param>
        public IEnumerable<ValidationAttribute> GetTypeValidationAttributes(ValidationContext objectContext)
            => GetTypeItem(objectContext.ObjectType).ValidationAttributes;

        /// <summary>
        /// Retrieves the display name associated with the given property.
        /// </summary>
        /// <param name="propertyContext">The context that describes the property.</param>
        public string GetPropertyDisplayName(ValidationContext propertyContext)
        {
            var typeItem = GetTypeItem(propertyContext.ObjectType);
            var propertyItem = typeItem.GetPropertyItem(propertyContext.MemberName);
            return propertyItem.DisplayName;
        }

        /// <summary>
        /// Retrieves the set of validation attributes for the property.
        /// </summary>
        /// <param name="propertyContext">The context that describes the property.</param>
        public IEnumerable<ValidationAttribute> GetPropertyValidationAttributes(ValidationContext propertyContext)
        {
            var typeItem = GetTypeItem(propertyContext.ObjectType);
            var propertyItem = typeItem.GetPropertyItem(propertyContext.MemberName);
            return propertyItem.ValidationAttributes;
        }

        private IStringLocalizer GetStringLocalizer(Type objectType)
        {
            IStringLocalizer stringLocalizer = null;
            var providerFactory = DataValidator.LocalizerProvider;

            if (stringLocalizerFactory != null && providerFactory != null)
            {
                stringLocalizer = providerFactory(objectType, stringLocalizerFactory);
            }

            return stringLocalizer;
        }

        private TypeItem GetTypeItem(Type type)
        {
            TypeItem item;
            if (!items.TryGetValue(type, out item))
            {
                var attributes = CustomAttributeExtensions.GetCustomAttributes(type.GetTypeInfo(), true);
                items[type] = item = new TypeItem(this, type, attributes);
            }
            return item;
        }

        private abstract class StoreItem
        {
            protected StoreItem(AttributeStore store)
            {
                Store = store;
            }

            public string DisplayName { get; protected set; }

            public IEnumerable<ValidationAttribute> ValidationAttributes { get; protected set; }

            protected AttributeStore Store { get; }

            protected string GetDisplayName(IEnumerable<Attribute> attributes, Type objectType, string propertyName = null)
            {
                var displayAttribute = attributes.OfType<DisplayAttribute>().FirstOrDefault();

                if (propertyName != null)
                {
                    var stringLocalizer = Store.GetStringLocalizer(objectType);

                    if (stringLocalizer != null && displayAttribute?.ResourceType == null)
                    {
                        return displayAttribute?.GetName() ?? stringLocalizer[propertyName];
                    }
                }

                return displayAttribute?.GetName();
            }

            protected IEnumerable<ValidationAttribute> GetValidationAttributes(IEnumerable<Attribute> attributes, Type objectType, string propertyName = null)
            {
                var results = new List<ValidationAttribute>();
                var stringLocalizer = Store.GetStringLocalizer(objectType);

                foreach (var attribute in attributes.OfType<ValidationAttribute>())
                {
                    if (stringLocalizer != null)
                    {
                        UpdateErrorMessage(stringLocalizer, attribute, propertyName);
                    }

                    results.Add(attribute);
                }

                return results;
            }

            private void UpdateErrorMessage(IStringLocalizer stringLocalizer, ValidationAttribute attribute, string propertyName)
            {
                if (CanUpdateErrorMessage(attribute))
                {
                    var validatorName = attribute.GetValidatorName();

                    attribute.ErrorMessage = propertyName != null
                        ? GetErrorMessage(stringLocalizer, propertyName, validatorName)
                        : stringLocalizer[validatorName];
                }
            }

            private string GetErrorMessage(IStringLocalizer stringLocalizer, string propertyName, string validatorName)
            {
                var localized = stringLocalizer[$"{propertyName}_{validatorName}"];
                return localized.ResourceNotFound ? stringLocalizer[$"@_{validatorName}"] : localized;
            }

            private bool CanUpdateErrorMessage(ValidationAttribute attribute)
                => string.IsNullOrEmpty(attribute.ErrorMessageResourceName) &&
                attribute.ErrorMessageResourceType == null;
        }

        private class TypeItem : StoreItem
        {
            private readonly Type objectType;
            private readonly ConcurrentDictionary<string, PropertyItem> propertyItems = new ConcurrentDictionary<string, PropertyItem>();

            public TypeItem(AttributeStore store, Type objectType, IEnumerable<Attribute> attributes)
                : base(store)
            {
                DisplayName = GetDisplayName(attributes, objectType);
                ValidationAttributes = GetValidationAttributes(attributes, objectType);
                this.objectType = objectType;
            }

            public PropertyItem GetPropertyItem(string propertyName)
            {
                if (propertyItems.Count == 0)
                {
                    AddPropertyItems();
                }

                if (!propertyItems.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"A property with name '{propertyName}' could not be found.");
                }

                return propertyItems[propertyName];
            }

            private void AddPropertyItems()
            {
                var properties = objectType.GetRuntimeProperties()
                    .Where(p => IsPublic(p) && p.GetIndexParameters().Empty());

                foreach (var property in properties)
                {
                    var attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);
                    propertyItems[property.Name] = new PropertyItem(Store, objectType, property.Name, attributes);
                }
            }

            private bool IsPublic(PropertyInfo property)
            {
                return property.GetMethod != null && property.GetMethod.IsPublic
                    || property.SetMethod != null && property.SetMethod.IsPublic;
            }
        }

        private class PropertyItem : StoreItem
        {
            public PropertyItem(AttributeStore store, Type objectType, string propertyName, IEnumerable<Attribute> attributes)
                : base(store)
            {
                DisplayName = GetDisplayName(attributes, objectType, propertyName);
                ValidationAttributes = GetValidationAttributes(attributes, objectType, propertyName);
            }
        }
    }
}