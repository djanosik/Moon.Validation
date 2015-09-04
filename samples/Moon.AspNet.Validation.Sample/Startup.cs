using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Moon.AspNet.Mvc;
using Moon.Localization;
using Moon.Localization.Validation;

namespace Moon.AspNet.Validation.Sample
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseLocalization(r =>
                r.LoadJson("resources"));

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddValidation(new ResourcesTextProvider());

            services.ConfigureMvcViews(o =>
            {
                o.ViewEngines.Clear();
                o.ViewEngines.Add(typeof(PagesViewEngine));
            });

            services.ConfigureRouting(o =>
            {
                o.LowercaseUrls = true;
                o.AppendTrailingSlash = true;
            });
        }
    }
}