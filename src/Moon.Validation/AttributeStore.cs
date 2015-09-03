using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Moon.Collections;

namespace Moon.Validation
{
    /// <summary>
    /// The cache of Validation and Display attributes.
    /// </summary>
    class AttributeStore
    {
        readonly IDictionary<Type, TypeItem> items = new Dictionary<Type, TypeItem>();

        /// <summary>
        /// Gets the singleton <see cref="AttributeStore" />
        /// </summary>
        public static AttributeStore Instance { get; } = new AttributeStore();

        /// <summary>
        /// Retrieves the display name associated with the given type.
        /// </summary>
        /// <param name="context">The context that describes the type.</param>
        public string GetTypeDisplayName(ValidationContext context)
        {
            var typeItem = GetTypeItem(context.ObjectType);
            return typeItem.DisplayAttribute.Name;
        }

        /// <summary>
        /// Retrieves the type level validation attributes for the given type.
        /// </summary>
        /// <param name="context">The context that describes the type.</param>
        public IEnumerable<ValidationAttribute> GetTypeValidationAttributes(ValidationContext context)
        {
            var typeItem = GetTypeItem(context.ObjectType);
            return typeItem.ValidationAttributes;
        }

        /// <summary>
        /// Retrieves the display name associated with the given property.
        /// </summary>
        /// <param name="context">The context that describes the property.</param>
        public string GetPropertyDisplayName(ValidationContext context)
        {
            var typeItem = GetTypeItem(context.ObjectType);
            var propertyItem = typeItem.GetPropertyItem(context.MemberName);
            return propertyItem.DisplayAttribute.Name;
        }

        /// <summary>
        /// Retrieves the set of validation attributes for the property.
        /// </summary>
        /// <param name="context">The context that describes the property.</param>
        public IEnumerable<ValidationAttribute> GetPropertyValidationAttributes(ValidationContext context)
        {
            var typeItem = GetTypeItem(context.ObjectType);
            var propertyItem = typeItem.GetPropertyItem(context.MemberName);
            return propertyItem.ValidationAttributes;
        }

        TypeItem GetTypeItem(Type type)
        {
            lock (items)
            {
                TypeItem item;
                if (!items.TryGetValue(type, out item))
                {
                    var attributes = CustomAttributeExtensions.GetCustomAttributes(type.GetTypeInfo(), true);
                    item = new TypeItem(type, attributes);
                    items[type] = item;
                }
                return item;
            }
        }

        abstract class StoreItem
        {
            public DisplayAttribute DisplayAttribute { get; protected set; }

            public IEnumerable<ValidationAttribute> ValidationAttributes { get; protected set; }

            protected DisplayAttribute GetDisplayAttribute(IEnumerable<Attribute> attributes, Type objectType, string propertyName = null)
            {
                var displayAttribute = attributes.OfType<DisplayAttribute>()
                    .FirstOrDefault() ?? new DisplayAttribute();

                if (!string.IsNullOrEmpty(displayAttribute.Name))
                {
                    return displayAttribute;
                }

                displayAttribute.ResourceType = null;

                displayAttribute.Name = !string.IsNullOrWhiteSpace(propertyName)
                    ? ValidationTexts.GetDisplayName(objectType, propertyName)
                    : ValidationTexts.GetDisplayName(objectType);

                return displayAttribute;
            }

            protected IEnumerable<ValidationAttribute> GetValidationAttributes(IEnumerable<Attribute> attributes, Type objectType, string propertyName = null)
            {
                var results = new List<ValidationAttribute>();

                var validationAttributes = attributes.OfType<ValidationAttribute>()
                    .Where(x => string.IsNullOrEmpty(x.ErrorMessage));

                foreach (var attribute in validationAttributes)
                {
                    var validatorName = attribute.GetValidatorName();
                    var messageKey = attribute.ErrorMessageResourceName;

                    attribute.ErrorMessageResourceType = null;
                    attribute.ErrorMessageResourceName = null;

                    attribute.ErrorMessage = !string.IsNullOrWhiteSpace(propertyName)
                        ? ValidationTexts.GetErrorMessage(objectType, propertyName, validatorName, messageKey)
                        : ValidationTexts.GetErrorMessage(objectType, validatorName, messageKey);

                    if (attribute.ErrorMessage == null)
                    {
                        continue;
                    }

                    results.Add(attribute);
                }

                return results;
            }
        }

        class TypeItem : StoreItem
        {
            readonly Type objectType;
            readonly object syncRoot = new object();
            Dictionary<string, PropertyItem> propertyItems;

            public TypeItem(Type objectType, IEnumerable<Attribute> attributes)
            {
                this.objectType = objectType;

                ValidationAttributes = GetValidationAttributes(attributes, objectType);
                DisplayAttribute = GetDisplayAttribute(attributes, objectType);
            }

            public PropertyItem GetPropertyItem(string propertyName)
            {
                if (propertyItems == null)
                {
                    lock (syncRoot)
                    {
                        if (propertyItems == null)
                        {
                            propertyItems = GetPropertyItems();
                        }
                    }
                }

                if (!propertyItems.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"A property with name '{propertyName}' could not be found.");
                }

                return propertyItems[propertyName];
            }

            Dictionary<string, PropertyItem> GetPropertyItems()
            {
                var items = new Dictionary<string, PropertyItem>();

                var properties = objectType.GetRuntimeProperties()
                    .Where(p => IsPublic(p) && p.GetIndexParameters().Empty());

                foreach (var property in properties)
                {
                    var attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);
                    items[property.Name] = new PropertyItem(objectType, property.Name, attributes);
                }

                return items;
            }

            bool IsPublic(PropertyInfo property)
            {
                return (property.GetMethod != null && property.GetMethod.IsPublic)
                    || (property.SetMethod != null && property.SetMethod.IsPublic);
            }
        }

        class PropertyItem : StoreItem
        {
            public PropertyItem(Type objectType, string propertyName, IEnumerable<Attribute> attributes)
            {
                ValidationAttributes = GetValidationAttributes(attributes, objectType, propertyName);
                DisplayAttribute = GetDisplayAttribute(attributes, objectType, propertyName);
            }
        }
    }
}