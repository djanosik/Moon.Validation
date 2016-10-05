using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Localization;
using Moon.Collections;

namespace Moon.Validation
{
    /// <summary>
    /// Asynchronous Data Annotations validator.
    /// </summary>
    public class DataValidator
    {
        /// <summary>
        /// The delegate to invoke for creating <see cref="IStringLocalizer" />.
        /// </summary>
        public static Func<Type, IStringLocalizerFactory, IStringLocalizer> LocalizerProvider;

        private readonly AttributeStore store;

        /// <summary>
        /// Initializes the <see cref="DataValidator" /> class.
        /// </summary>
        static DataValidator()
        {
            LocalizerProvider = (objectType, stringLocalizerFactory) => stringLocalizerFactory.Create(objectType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidator" /> class.
        /// </summary>
        public DataValidator()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataValidator" /> class.
        /// </summary>
        /// <param name="stringLocalizerFactory">The string localizer factory.</param>
        public DataValidator(IStringLocalizerFactory stringLocalizerFactory)
        {
            store = new AttributeStore(stringLocalizerFactory);
        }

        /// <summary>
        /// Returns whether the given object is valid or not.
        /// </summary>
        /// <param name="instance">The object to test. It cannot be null.</param>
        /// <param name="validateRecursively">A value indicating whether to validate all nested objects recursively.</param>
        public bool IsValid(object instance, bool validateRecursively = false)
            => !Validate(instance, validateRecursively).Any();

        /// <summary>
        /// Validates the given object and returns an enumeration of results for the failures.
        /// </summary>
        /// <param name="instance">The object to test. It cannot be null.</param>
        /// <param name="validateRecursively">A value indicating whether to validate all nested objects recursively.</param>
        public IEnumerable<ValidationResult> Validate(object instance, bool validateRecursively = false)
        {
            Requires.NotNull(instance, nameof(instance));

            return Validate(CreateObjectContext(instance), validateRecursively);
        }

        private IEnumerable<ValidationResult> Validate(ValidationContext objectContext, bool validateRecursively)
        {
            var results = new List<ValidationResult>();

            // Step 1: Validate the object properties' validation attributes
            results.AddRange(ValidateProperties(objectContext, validateRecursively));

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

            // Step 3: Execute IValidatableObject.Validate implementation
            var validatable = objectContext.ObjectInstance as IValidatableObject;

            if (validatable != null)
            {
                results.AddRange(validatable.Validate(objectContext)
                    .Select(x => new ValidationResult(x.ErrorMessage, AppendMemberNames(objectContext, x.MemberNames)))
                    .Where(x => x != ValidationResult.Success));
            }

            return results;
        }

        private IEnumerable<ValidationResult> ValidateProperties(ValidationContext objectContext, bool validateRecursively)
        {
            var results = new List<ValidationResult>();
            var propsToValidate = GetPropertiesToValidate(objectContext, validateRecursively);

            foreach (var property in propsToValidate)
            {
                var propertyResults = new List<ValidationResult>();
                var attributes = store.GetPropertyValidationAttributes(property.Context);

                propertyResults.AddRange(GetResults(property.Value, property.Context, attributes));

                if (propertyResults.Count == 0 && property.Value != null && validateRecursively)
                {
                    var memberName = property.Context.MemberName;
                    var propertyObjectContext = CreatePropertyObjectContext(objectContext, memberName, property.Value);

                    propertyResults.AddRange(Validate(propertyObjectContext, true));
                }

                results.AddRange(propertyResults);
            }

            return results;
        }

        private IEnumerable<ValidationResult> ValidateObject(ValidationContext objectContext)
        {
            var results = new List<ValidationResult>();

            var attributes = store.GetTypeValidationAttributes(objectContext);
            results.AddRange(GetResults(objectContext.ObjectInstance, objectContext, attributes));

            return results;
        }

        private IEnumerable<PropertyToValidate> GetPropertiesToValidate(ValidationContext objectContext, bool validateRecursively)
        {
            var properties = objectContext.ObjectType.GetRuntimeProperties()
                .Where(p => IsPublic(p) && p.GetIndexParameters().Empty());

            foreach (var property in properties)
            {
                var context = CreatePropertyContext(objectContext, property.Name);

                if (validateRecursively || store.GetPropertyValidationAttributes(context).Any())
                {
                    yield return new PropertyToValidate {
                        Context = context,
                        Value = property.GetValue(objectContext.ObjectInstance, null)
                    };
                }
            }
        }

        private IEnumerable<ValidationResult> GetResults(object value, ValidationContext validationContext, IEnumerable<ValidationAttribute> attributes)
        {
            var results = new List<ValidationResult>();
            ValidationResult result;

            // Get the required validator; if there is one and test it first, aborting on failure
            var required = attributes.OfType<RequiredAttribute>().FirstOrDefault();

            if (required != null && !TryValidate(value, validationContext, required, out result))
            {
                results.Add(result);
                return results;
            }

            // Iterate through the rest of the validators, skipping the required validator
            foreach (var attribute in attributes)
            {
                attribute.SetOtherDisplayName(name => store.GetPropertyDisplayName(CreatePropertyContext(validationContext, name)));

                if (attribute != required && !TryValidate(value, validationContext, attribute, out result))
                {
                    results.Add(result);
                }
            }

            return results;
        }

        private ValidationContext CreateObjectContext(object instance)
        {
            var context = new ValidationContext(instance);
            context.DisplayName = store.GetTypeDisplayName(context);
            return context;
        }

        private ValidationContext CreatePropertyObjectContext(ValidationContext objectContext, string memberName, object value)
        {
            var context = new ValidationContext(value, objectContext.Items) { MemberName = memberName };
            context.DisplayName = store.GetTypeDisplayName(context);
            return context;
        }

        private ValidationContext CreatePropertyContext(ValidationContext objectContext, string propertyName)
        {
            var memberName = AppendMemberName(objectContext, propertyName);
            var context = new ValidationContext(objectContext.ObjectInstance, objectContext.Items) { MemberName = memberName };
            context.DisplayName = store.GetPropertyDisplayName(context);
            return context;
        }

        private IEnumerable<string> AppendMemberNames(ValidationContext objectContext, IEnumerable<string> memberNames)
            => memberNames.Select(n => AppendMemberName(objectContext, n));

        private string AppendMemberName(ValidationContext objectContext, string memberName)
            => objectContext.MemberName != null ? $"{objectContext.MemberName}.{memberName}" : memberName;

        private bool TryValidate(object value, ValidationContext validationContext, ValidationAttribute attribute, out ValidationResult result)
        {
            result = attribute.GetValidationResult(value, validationContext);

            if (result == ValidationResult.Success)
            {
                result = null;
                return true;
            }

            return false;
        }

        private bool IsPublic(PropertyInfo property)
        {
            return property.GetMethod != null && property.GetMethod.IsPublic ||
                property.SetMethod != null && property.SetMethod.IsPublic;
        }

        private class PropertyToValidate
        {
            public object Value { get; set; }

            public ValidationContext Context { get; set; }
        }
    }
}