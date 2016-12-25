using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.Extensions.Localization;
using Moon.AspNetCore.Validation.Adapters;
using Moon.Validation;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Provides Data Annotations adapters for Moon.Validation attributes.
    /// </summary>
    public class MoonValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly Dictionary<Type, AdapterFactory> factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoonValidationAttributeAdapterProvider" /> class.
        /// </summary>
        public MoonValidationAttributeAdapterProvider()
        {
            factories = GetAdapterFactories();
        }

        /// <summary>
        /// Creates an <see cref="IAttributeAdapter" /> for the given attribute.
        /// </summary>
        /// <param name="attribute">The attribute to create an adapter for.</param>
        /// <param name="stringLocalizer">The localizer to provide to the adapter.</param>
        /// <returns>An <see cref="IAttributeAdapter" /> for the given attribute.</returns>
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            Requires.NotNull(attribute, nameof(attribute));

            AdapterFactory factory;

            if (factories.TryGetValue(attribute.GetType(), out factory))
            {
                return factory(attribute, stringLocalizer);
            }

            var inner = new ValidationAttributeAdapterProvider();
            return inner.GetAttributeAdapter(attribute, stringLocalizer);
        }

        private static Dictionary<Type, AdapterFactory> GetAdapterFactories()
        {
            return new Dictionary<Type, AdapterFactory> {
                [typeof(DigitsAttribute)] = (attr, localizer) => new DigitsAttributeAdapter((DigitsAttribute)attr, localizer),
                [typeof(DoubleAttribute)] = (attr, localizer) => new DoubleAttributeAdapter((DoubleAttribute)attr, localizer),
                [typeof(EqualAttribute)] = (attr, localizer) => new CompareAttributeAdapter<EqualAttribute>((EqualAttribute)attr, localizer),
                [typeof(FloatAttribute)] = (attr, localizer) => new FloatAttributeAdapter((FloatAttribute)attr, localizer),
                [typeof(GreaterThanAttribute)] = (attr, localizer) => new CompareAttributeAdapter<GreaterThanAttribute>((GreaterThanAttribute)attr, localizer),
                [typeof(GreaterThanOrEqualAttribute)] = (attr, localizer) => new CompareAttributeAdapter<GreaterThanOrEqualAttribute>((GreaterThanOrEqualAttribute)attr, localizer),
                [typeof(IntegerAttribute)] = (attr, localizer) => new IntegerAttributeAdapter((IntegerAttribute)attr, localizer),
                [typeof(LessThanAttribute)] = (attr, localizer) => new CompareAttributeAdapter<LessThanAttribute>((LessThanAttribute)attr, localizer),
                [typeof(LessThanOrEqualAttribute)] = (attr, localizer) => new CompareAttributeAdapter<LessThanOrEqualAttribute>((LessThanOrEqualAttribute)attr, localizer),
                [typeof(MaxAttribute)] = (attr, localizer) => new MaxAttributeAdapter((MaxAttribute)attr, localizer),
                [typeof(MinAttribute)] = (attr, localizer) => new MinAttributeAdapter((MinAttribute)attr, localizer),
                [typeof(NotEqualAttribute)] = (attr, localizer) => new CompareAttributeAdapter<NotEqualAttribute>((NotEqualAttribute)attr, localizer),
                [typeof(RequiredIfEmptyAttribute)] = (attr, localizer) => new RequiredIfEmptyAttributeAdapter((RequiredIfEmptyAttribute)attr, localizer),
                [typeof(RequiredIfEqualAttribute)] = (attr, localizer) => new RequiredIfAttributeAdapter<RequiredIfEqualAttribute>((RequiredIfEqualAttribute)attr, localizer),
                [typeof(RequiredIfGreaterThanAttribute)] = (attr, localizer) => new RequiredIfAttributeAdapter<RequiredIfGreaterThanAttribute>((RequiredIfGreaterThanAttribute)attr, localizer),
                [typeof(RequiredIfGreaterThanOrEqualAttribute)] = (attr, localizer) => new RequiredIfAttributeAdapter<RequiredIfGreaterThanOrEqualAttribute>((RequiredIfGreaterThanOrEqualAttribute)attr, localizer),
                [typeof(RequiredIfLessThanAttribute)] = (attr, localizer) => new RequiredIfAttributeAdapter<RequiredIfLessThanAttribute>((RequiredIfLessThanAttribute)attr, localizer),
                [typeof(RequiredIfLessThanOrEqualAttribute)] = (attr, localizer) => new RequiredIfAttributeAdapter<RequiredIfLessThanOrEqualAttribute>((RequiredIfLessThanOrEqualAttribute)attr, localizer),
                [typeof(RequiredIfNotEmptyAttribute)] = (attr, localizer) => new RequiredIfNotEmptyAttributeAdapter((RequiredIfNotEmptyAttribute)attr, localizer),
                [typeof(RequiredIfNotEqualAttribute)] = (attr, localizer) => new RequiredIfAttributeAdapter<RequiredIfNotEqualAttribute>((RequiredIfNotEqualAttribute)attr, localizer)
            };
        }

        private delegate IAttributeAdapter AdapterFactory(ValidationAttribute attribute,
            IStringLocalizer stringLocalizer);
    }
}