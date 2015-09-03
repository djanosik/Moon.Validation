using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Moon.Collections;

namespace Moon.Validation
{
    /// <summary>
    /// Better Data Annotations validator.
    /// </summary>
    public static class MoonValidator
    {
        static readonly AttributeStore store = AttributeStore.Instance;

        /// <summary>
        /// Returns whether the given object is valid or not.
        /// </summary>
        /// <param name="instance">The object to test. It cannot be null.</param>
        public static bool IsValid(object instance)
            => !Validate(instance).Any();

        /// <summary>
        /// Returns whether the given properties are valid or not.
        /// </summary>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <param name="instance">The object properties are defined on. It cannot be null.</param>
        /// <param name="properties">
        /// An array of properties to test. It can be empty, but cannot be null.
        /// </param>
        public static bool IsValid<TInstance>(TInstance instance, params Expression<Func<TInstance, object>>[] properties)
            where TInstance : class
            => Validate(instance, properties).Empty();

        /// <summary>
        /// Returns whether the given properties are valid or not.
        /// </summary>
        /// <param name="instance">The object properties are defined on. It cannot be null.</param>
        /// <param name="propertyNames">
        /// An array of properties to test. It can be empty, but cannot be null.
        /// </param>
        public static bool IsValid(object instance, params string[] propertyNames)
            => Validate(instance, propertyNames).Empty();

        /// <summary>
        /// Validates the given object and returns an enumeration of <see cref="ValidationResult" />
        /// s for the failures.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method evaluates all <see cref="ValidationAttribute" /> s attached to the object
        /// instance's type or it's properties. It will also execute the
        /// <see cref="IValidatable.Validate" /> method.
        /// </para>
        /// <para>
        /// For any given property, if it has a <see cref="RequiredAttribute" /> that fails
        /// validation, no other validators will be evaluated for that property.
        /// </para>
        /// </remarks>
        /// <param name="instance">The object to test. It cannot be null.</param>
        public static IEnumerable<ValidationResult> Validate(object instance)
        {
            Requires.NotNull(instance, nameof(instance));

            var results = new List<ValidationResult>();
            var objectContext = CreateObjectContext(instance);

            // Step 1: Validate the object properties' validation attributes
            results.AddRange(ValidateProperties(objectContext));

            if (results.Any())
            {
                return results;
            }

            // Step 2: Validate the object's validation attributes
            results.AddRange(ValidateObject(objectContext));

            if (results.Any())
            {
                return results;
            }

            // Step 3: Execute IValidatable.Validate implementation
            var validatable = instance as IValidatable;

            if (validatable != null)
            {
                results.AddRange(validatable.Validate(objectContext)
                    .Where(x => x != ValidationResult.Success));
            }

            return results;
        }

        /// <summary>
        /// Validates the given properties and returns an enumeration of
        /// <see cref="ValidationResult" /> s for the failures.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For any given property, if it has a <see cref="RequiredAttribute" /> that fails
        /// validation, no other validators will be evaluated for that property.
        /// </para>
        /// </remarks>
        /// <typeparam name="TInstance">The type of the instance.</typeparam>
        /// <param name="instance">The object properties are defined on. It cannot be null.</param>
        /// <param name="properties">
        /// An array of properties to test. It can be empty, but cannot be null.
        /// </param>
        public static IEnumerable<ValidationResult> Validate<TInstance>(TInstance instance, params Expression<Func<TInstance, object>>[] properties) where TInstance : class
        {
            Requires.NotNull(instance, nameof(instance));
            Requires.NotNull(properties, nameof(properties));

            if (properties.Length == 0)
            {
                return Enumerable.Empty<ValidationResult>();
            }

            var propertyNames = new List<string>();

            foreach (var expression in properties)
            {
                var memberExpression = expression.Body as MemberExpression;

                if (expression.NodeType != ExpressionType.Lambda || memberExpression == null || !(memberExpression.Member is PropertyInfo))
                {
                    throw new ArgumentException("Only Lambda expressions accessing properties are supported.", nameof(properties));
                }

                propertyNames.Add(memberExpression.Member.Name);
            }

            return Validate(instance, propertyNames.ToArray());
        }

        /// <summary>
        /// Validates the given properties and returns an enumeration of
        /// <see cref="ValidationResult" /> s for the failures.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For any given property, if it has a <see cref="RequiredAttribute" /> that fails
        /// validation, no other validators will be evaluated for that property.
        /// </para>
        /// </remarks>
        /// <param name="instance">The object properties are defined on. It cannot be null.</param>
        /// <param name="propertyNames">
        /// An array of properties to test. It can be empty, but cannot be null.
        /// </param>
        public static IEnumerable<ValidationResult> Validate(object instance, params string[] propertyNames)
        {
            Requires.NotNull(instance, nameof(instance));
            Requires.NotNull(propertyNames, nameof(propertyNames));

            if (propertyNames.Length == 0)
            {
                return Enumerable.Empty<ValidationResult>();
            }

            return ValidateProperties(CreateObjectContext(instance), propertyNames);
        }

        static ValidationContext CreateObjectContext(object instance)
        {
            var context = new ValidationContext(instance);
            context.DisplayName = store.GetTypeDisplayName(context);
            return context;
        }

        static ValidationContext CreatePropertyContext(ValidationContext objectContext, string propertyName)
        {
            var context = new ValidationContext(objectContext.ObjectInstance, objectContext.Items)
            {
                MemberName = propertyName
            };
            context.DisplayName = store.GetPropertyDisplayName(context);
            return context;
        }

        static IEnumerable<PropertyToValidate> GetPropertiesToValidate(ValidationContext objectContext)
        {
            var properties = objectContext.ObjectType.GetRuntimeProperties()
                .Where(p => IsPublic(p) && p.GetIndexParameters().Empty());

            foreach (var property in properties)
            {
                var context = CreatePropertyContext(objectContext, property.Name);

                if (store.GetPropertyValidationAttributes(context).Any())
                {
                    yield return new PropertyToValidate
                    {
                        Context = context,
                        Value = property.GetValue(objectContext.ObjectInstance, null)
                    };
                }
            }
        }

        static IEnumerable<PropertyToValidate> GetPropertiesToValidate(ValidationContext objectContext, params string[] propertyNames)
        {
            var properties = propertyNames
                .Select(n => objectContext.ObjectType.GetRuntimeProperty(n))
                .Where(IsPublic);

            foreach (var property in properties)
            {
                var context = CreatePropertyContext(objectContext, property.Name);

                if (store.GetPropertyValidationAttributes(context).Any())
                {
                    yield return new PropertyToValidate
                    {
                        Context = context,
                        Value = property.GetValue(objectContext.ObjectInstance, null)
                    };
                }
            }
        }

        static IEnumerable<ValidationResult> GetResults(object value, ValidationContext context, IEnumerable<ValidationAttribute> attributes)
        {
            var results = new List<ValidationResult>();
            ValidationResult result;

            // Get the required validator if there is one and test it first, aborting on failure
            var required = attributes.OfType<RequiredAttribute>().FirstOrDefault();

            if (required != null && !TryValidate(value, context, required, out result))
            {
                results.Add(result);
                return results;
            }

            // Iterate through the rest of the validators, skipping the required validator
            foreach (var attribute in attributes)
            {
                if (attribute != required && !TryValidate(value, context, attribute, out result))
                {
                    results.Add(result);
                }
            }

            return results;
        }

        static bool TryValidate(object value, ValidationContext context, ValidationAttribute attribute, out ValidationResult result)
        {
            result = attribute.GetValidationResult(value, context);

            if (result == ValidationResult.Success)
            {
                result = null;
                return true;
            }

            return false;
        }

        static IEnumerable<ValidationResult> ValidateObject(ValidationContext objectContext)
        {
            var results = new List<ValidationResult>();

            var attributes = store.GetTypeValidationAttributes(objectContext);
            results.AddRange(GetResults(objectContext.ObjectInstance, objectContext, attributes));

            return results;
        }

        static IEnumerable<ValidationResult> ValidateProperties(ValidationContext objectContext)
        {
            var results = new List<ValidationResult>();
            var propsToValidate = GetPropertiesToValidate(objectContext);

            foreach (var property in propsToValidate)
            {
                var attributes = store.GetPropertyValidationAttributes(property.Context);
                results.AddRange(GetResults(property.Value, property.Context, attributes));
            }

            return results;
        }

        static IEnumerable<ValidationResult> ValidateProperties(ValidationContext objectContext, params string[] propertyNames)
        {
            var results = new List<ValidationResult>();
            var propsToValidate = GetPropertiesToValidate(objectContext, propertyNames);

            foreach (var property in propsToValidate)
            {
                var attributes = store.GetPropertyValidationAttributes(property.Context);
                results.AddRange(GetResults(property.Value, property.Context, attributes));
            }

            return results;
        }

        static bool IsPublic(PropertyInfo property)
        {
            return (property.GetMethod != null && property.GetMethod.IsPublic)
                || (property.SetMethod != null && property.SetMethod.IsPublic);
        }

        class PropertyToValidate
        {
            public ValidationContext Context { get; set; }

            public object Value { get; set; }
        }
    }
}