using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Moon.Validation;

namespace Moon.Web.Validation
{
    /// <summary>
    /// The convention-based model metadata provider fro ASP.NET MVC.
    /// </summary>
    public class ValidationMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        readonly ITextProvider textProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMetadataProvider" /> class.
        /// </summary>
        /// <param name="textProvider">The validation text provider.</param>
        public ValidationMetadataProvider(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }

        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">An enumeration of attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            attributes = LocalizeDisplayName(attributes, containerType, modelType, propertyName);
            return base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
        }

        IEnumerable<Attribute> LocalizeDisplayName(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
        {
            var objectType = containerType ?? modelType;

            var displayAttribute = attributes.OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (displayAttribute == null)
            {
                displayAttribute = new DisplayAttribute();
                attributes = attributes.Union(new[] { displayAttribute });
            }

            if (!string.IsNullOrEmpty(displayAttribute.Name))
            {
                return attributes;
            }

            displayAttribute.ResourceType = null;

            displayAttribute.Name = propertyName != null
                ? textProvider.GetDisplayName(objectType, propertyName)
                : textProvider.GetDisplayName(objectType);

            return attributes;
        }
    }
}