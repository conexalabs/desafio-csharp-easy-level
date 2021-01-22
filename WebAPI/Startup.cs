using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using WebAPI.Data.Context;
using WebAPI.Data.Interfaces;
using WebAPI.Data.Interfaces.Service.Models.Entities;
using WebAPI.Data.Repository;
using WebAPI.Data.Service.Models.Entities;
using WebAPI.Domain.Interfaces.Response.CityWeather;
using WebAPI.Domain.Interfaces.Service.OpenWeather;
using WebAPI.Domain.Interfaces.Service.Validation;
using WebAPI.Domain.Models.Response;
using WebAPI.Domain.Services.ActionResult;
using WebAPI.Domain.Services.OpenWeather;
using WebAPI.Domain.Services.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();
            services.AddScoped<ICityWeatherActionResult, CityWeatherActionResult>();
            services.AddScoped<ICityHystoricalWeatherService, CityHystoricalWeatherService>();
            services.AddScoped<IValidations, Validations>();
            services.AddControllers();

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version = "v1",
                    Title = "WebAPI",
                    Description = "Desafio Backend do Hub Conexa em C# - Easy Level",
                    TermsOfService = new Uri("https://github.com/conexalabs/desafio-csharp-easy-level"),
                    Contact = new OpenApiContact
                    {
                        Name = "Diego dos S. Soares",
                        Email = "dieg657@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/diego-soares-148970151/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GNU General Public License 3.0",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.pt-br.html"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                c.RoutePrefix = "api/documentation";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
