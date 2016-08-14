using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        /// <param name="stringLocalizerFactory">The string localizer factory.</param>
        public DataValidator(IStringLocalizerFactory stringLocalizerFactory)
        {
            Requires.NotNull(stringLocalizerFactory, nameof(stringLocalizerFactory));
            store = new AttributeStore(stringLocalizerFactory);
        }

        /// <summary>
        /// Returns whether the given object is valid or not.
        /// </summary>
        /// <param name="instance">The object to test. It cannot be null.</param>
        public async Task<bool> IsValidAsync(object instance)
        {
            var results = await ValidateAsync(instance);
            return !results.Any();
        }

        /// <summary>
        /// Validates the given object and returns an enumeration of results for the failures.
        /// </summary>
        /// <param name="instance">The object to test. It cannot be null.</param>
        public async Task<IEnumerable<ValidationResult>> ValidateAsync(object instance)
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

            // Step 3: Execute IAsyncValidatableObject.ValidateAsync implementation
            var asyncValidatable = instance as IAsyncValidatableObject;

            if (asyncValidatable != null)
            {
                var asyncResults = await asyncValidatable.ValidateAsync(objectContext);
                results.AddRange(asyncResults.Where(x => x != ValidationResult.Success));
            }

            // Step 4: Execute IValidatableObject.Validate implementation
            var validatable = instance as IValidatableObject;

            if (validatable != null)
            {
                var syncResults = validatable.Validate(objectContext);
                results.AddRange(syncResults.Where(x => x != ValidationResult.Success));
            }

            return results;
        }

        private IEnumerable<ValidationResult> ValidateProperties(ValidationContext objectContext)
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

        private IEnumerable<ValidationResult> ValidateObject(ValidationContext objectContext)
        {
            var results = new List<ValidationResult>();

            var attributes = store.GetTypeValidationAttributes(objectContext);
            results.AddRange(GetResults(objectContext.ObjectInstance, objectContext, attributes));

            return results;
        }

        private IEnumerable<PropertyToValidate> GetPropertiesToValidate(ValidationContext objectContext)
        {
            var properties = objectContext.ObjectType.GetRuntimeProperties()
                .Where(p => IsPublic(p) && p.GetIndexParameters().Empty());

            foreach (var property in properties)
            {
                var context = CreatePropertyContext(objectContext, property.Name);

                if (store.GetPropertyValidationAttributes(context).Any())
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

        private ValidationContext CreatePropertyContext(ValidationContext objectContext, string propertyName)
        {
            var context = new ValidationContext(objectContext.ObjectInstance, objectContext.Items) { MemberName = propertyName };
            context.DisplayName = store.GetPropertyDisplayName(context);
            return context;
        }

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