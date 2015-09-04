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
        readonly ITextProvider textProvider;
        readonly IDictionary<Type, TypeItem> items = new Dictionary<Type, TypeItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeStore" /> class.
        /// </summary>
        /// <param name="textProvider">The validation text provider.</param>
        public AttributeStore(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }

        /// <summary>
        /// Retrieves the display name associated with the given type.
        /// </summary>
        /// <param name="objectContext">The context that describes the type.</param>
        public string GetTypeDisplayName(ValidationContext objectContext)
        {
            var typeItem = GetTypeItem(objectContext.ObjectType);
            return typeItem.DisplayAttribute.Name;
        }

        /// <summary>
        /// Retrieves the type level validation attributes for the given type.
        /// </summary>
        /// <param name="objectContext">The context that describes the type.</param>
        public IEnumerable<ValidationAttribute> GetTypeValidationAttributes(ValidationContext objectContext)
        {
            var typeItem = GetTypeItem(objectContext.ObjectType);
            return typeItem.ValidationAttributes;
        }

        /// <summary>
        /// Retrieves the display name associated with the given property.
        /// </summary>
        /// <param name="propertyContext">The context that describes the property.</param>
        public string GetPropertyDisplayName(ValidationContext propertyContext)
        {
            var typeItem = GetTypeItem(propertyContext.ObjectType);
            var propertyItem = typeItem.GetPropertyItem(propertyContext.MemberName);
            return propertyItem.DisplayAttribute.Name;
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

        TypeItem GetTypeItem(Type type)
        {
            lock (items)
            {
                TypeItem item;
                if (!items.TryGetValue(type, out item))
                {
                    var attributes = CustomAttributeExtensions.GetCustomAttributes(type.GetTypeInfo(), true);
                    items[type] = item = new TypeItem(type, attributes, textProvider);
                }
                return item;
            }
        }

        abstract class StoreItem
        {
            protected StoreItem(ITextProvider textProvider)
            {
                TextProvider = textProvider;
            }

            public DisplayAttribute DisplayAttribute { get; protected set; }

            public IEnumerable<ValidationAttribute> ValidationAttributes { get; protected set; }

            public ITextProvider TextProvider { get; }

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
                    ? TextProvider.GetDisplayName(objectType, propertyName)
                    : TextProvider.GetDisplayName(objectType);

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
                        ? TextProvider.GetErrorMessage(objectType, propertyName, validatorName, messageKey)
                        : TextProvider.GetErrorMessage(objectType, validatorName, messageKey);

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

            public TypeItem(Type objectType, IEnumerable<Attribute> attributes, ITextProvider textProvider)
                : base(textProvider)
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
                    items[property.Name] = new PropertyItem(objectType, property.Name, attributes, TextProvider);
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
            public PropertyItem(Type objectType, string propertyName, IEnumerable<Attribute> attributes, ITextProvider textProvider)
                : base(textProvider)
            {
                ValidationAttributes = GetValidationAttributes(attributes, objectType, propertyName);
                DisplayAttribute = GetDisplayAttribute(attributes, objectType, propertyName);
            }
        }
    }
}