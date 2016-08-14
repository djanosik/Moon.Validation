using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Validation
{
    /// <summary>
    /// Sets up default options for <see cref="MvcViewOptions" /> for Moon.Validation.
    /// </summary>
    public class MoonValidationMvcViewOptionsSetup : ConfigureOptions<MvcViewOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoonValidationMvcViewOptionsSetup" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MoonValidationMvcViewOptionsSetup(IServiceProvider serviceProvider)
            : base(options => Configure(options, serviceProvider))
        {
        }

        /// <summary>
        /// Configures the MVC view options for Moon.Validation attributes.
        /// </summary>
        /// <param name="options">The MVC options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public static void Configure(MvcViewOptions options, IServiceProvider serviceProvider)
        {
            var numericProvider = options.ClientModelValidatorProviders
                .OfType<NumericClientModelValidatorProvider>()
                .FirstOrDefault();

            if (numericProvider != null)
            {
                // it would conflict with our client-side validator for Double and Float attributes
                options.ClientModelValidatorProviders.Remove(numericProvider);
            }
        }
    }
}