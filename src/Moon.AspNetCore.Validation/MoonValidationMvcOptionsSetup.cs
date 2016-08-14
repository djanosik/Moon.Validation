using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Sets up default options for <see cref="MvcOptions" /> for Moon.Validation.
    /// </summary>
    public class MoonValidationMvcOptionsSetup : ConfigureOptions<MvcOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoonValidationMvcOptionsSetup" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MoonValidationMvcOptionsSetup(IServiceProvider serviceProvider)
            : base(options => Configure(options, serviceProvider))
        {
        }

        /// <summary>
        /// Configures the MVC options for Moon.Validation attributes.
        /// </summary>
        /// <param name="options">The MVC options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Configure(MvcOptions options, IServiceProvider serviceProvider)
        {
            var stringLocalizerFactory = serviceProvider.GetService<IStringLocalizerFactory>();
            var localizationOptions = serviceProvider.GetRequiredService<IOptions<MvcDataAnnotationsLocalizationOptions>>();

            options.ModelMetadataDetailsProviders.Add(new MoonValidationMetadataProvider(
                localizationOptions,
                stringLocalizerFactory));
        }
    }
}