using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Moon.AspNet.Mvc;
using Moon.Localization;
using Moon.Localization.Validation;

namespace Moon.AspNet.Validation.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.UseLocalization(r =>
                r.LoadJson("resources"));

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddValidation(new LocalizationTextProvider());

            services.AddSingleton<IRazorViewEngine, PagesViewEngine>();

            services.ConfigureRouting(o =>
            {
                o.LowercaseUrls = true;
                o.AppendTrailingSlash = true;
            });
        }
    }
}