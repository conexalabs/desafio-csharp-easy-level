using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Siagri.Clima.API.Configurations;
using Siagri.Clima.Business.Constants;

namespace Siagri.Clima.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SiagriSettings.Initialize(configuration.GetSection("Settings").Get<SiagriSettings>());
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfigurations(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiConfigurations(env);
        }
    }
}
