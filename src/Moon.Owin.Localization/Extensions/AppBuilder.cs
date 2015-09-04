using System.Web.Mvc;
using Moon;
using Moon.Validation;
using Moon.Web.Validation;

namespace Owin
{
    /// <summary>
    /// <see cref="IAppBuilder" /> extension methods.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds services required to use localizable validation attributes.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="textProvider">The validation text provider.</param>
        public static void AddValidation(this IAppBuilder app, ITextProvider textProvider)
        {
            Requires.NotNull(textProvider, nameof(textProvider));

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ValidationModelValidatorProvider(textProvider));
            ModelMetadataProviders.Current = new ValidationMetadataProvider(textProvider);
            AttributeAdapters.Register();
        }
    }
}