using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Moon.AspNetCore.Validation.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<RazorViewEngineOptions>(o =>
                {
                    o.ViewLocationFormats.Clear();
                    o.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
                })
                .AddLocalization(o => o.ResourcesPath = "Resources")
                .AddMvc()
                .AddDataAnnotationsLocalization()
                .AddMoonValidation();
        }

        public void Configure(IApplicationBuilder app)
        {
            var cultures = new[] {
                new CultureInfo("en"),
                new CultureInfo("cs")
            };

            app
                .UseRequestLocalization(new RequestLocalizationOptions {
                    DefaultRequestCulture = new RequestCulture("en"),
                    SupportedCultures = cultures,
                    SupportedUICultures = cultures
                })
                .UseMvc();
        }
    }
}